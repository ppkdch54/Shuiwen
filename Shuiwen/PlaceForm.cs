using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Teigha;
using Teigha.DatabaseServices;
using Teigha.GraphicsSystem;
using Teigha.Runtime;
using Teigha.GraphicsInterface;
using Teigha.Geometry;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using Microsoft.VisualBasic;
using ShuiwenLib;

namespace Shuiwen
{
    public partial class PlaceForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        enum Mode
        {
            Quiescent,
            Selection,
            DragDrop
        }

        public static Teigha.Runtime.Services dd;
        Graphics graphics;
        Teigha.GraphicsSystem.LayoutHelperDevice helperDevice;
        Database database = null;
        //   ObjectIdCollection selected = new ObjectIdCollection();
        //Point2d startSelPoint;
        Point3d firstCornerPoint;

        LayoutManager lm;
        int bZoomWindow = -1;

        public PlaceForm(Sites sites,bool edit)
        {
            this.sites = sites;
            if (dd == null)
            {
                dd = new Teigha.Runtime.Services();
                SystemObjects.DynamicLinker.LoadApp("GripPoints", false, false);
                SystemObjects.DynamicLinker.LoadApp("PlotSettingsValidator", false, false);
            }
            InitializeComponent();
            splitContainer1.Panel2.MouseWheel += new MouseEventHandler(Form1_MouseWheel);

            //HostApplicationServices.Current = new HostAppServ(dd);
            //Environment.SetEnvironmentVariable("DDPLOTSTYLEPATHS", ((HostAppServ)HostApplicationServices.Current).FindConfigPath(String.Format("PrinterStyleSheetDir")));

            //    gripManager = new ExGripManager();
            this.bEdit = edit;
            if (!bEdit)
            {
                buttonImport.Visible = false;
                buttonDel.Visible = false;
                listBoxPaper.Height += 50;
                splitContainer1.Panel2.DragEnter -= splitContainer1_Panel2_DragEnter;
                splitContainer1.Panel2.DragDrop -= splitContainer1_Panel2_DragDrop;
                treeViewSensors.MouseDown -= treeViewSensors_MouseDown;
                //treeViewSensors.NodeMouseClick -= treeViewSensors_NodeMouseClick;
            }
            else
            {
                treeViewSensors.AfterSelect -= treeViewSensors_AfterSelect;
            }
           
            //DisableAero();
        }

        private Point3d toEyeToWorld(int x, int y)
        {
            using (Teigha.GraphicsSystem.View pView = helperDevice.ActiveView)
            {
                Point3d wcsPt = new Point3d(x, y, 0.0);
                wcsPt = wcsPt.TransformBy((pView.ScreenMatrix * pView.ProjectionMatrix).Inverse());
                wcsPt = new Point3d(wcsPt.X, wcsPt.Y, 0.0);
                using (AbstractViewPE pVpPE = new AbstractViewPE(pView))
                {
                    return wcsPt.TransformBy(pVpPE.EyeToWorld);
                }
            }
        }

        private Point ToClient(Point3d pt3d)
        {
            using (Teigha.GraphicsSystem.View pView = helperDevice.ActiveView)
            {
                Point3d ptDvc = pt3d.TransformBy(pView.WorldToDeviceMatrix);
                return new Point((int)ptDvc.X, (int)ptDvc.Y);
            }
        }

        // helper function transforming parameters from screen to world coordinates
        void dolly(Teigha.GraphicsSystem.View pView, int x, int y)
        {
            Vector3d vec = new Vector3d(-x, -y, 0.0);
            vec = vec.TransformBy((pView.ScreenMatrix * pView.ProjectionMatrix).Inverse());
            pView.Dolly(vec);
        }
        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            using (Teigha.GraphicsSystem.View pView = helperDevice.ActiveView)
            {
                // camera position in world coordinates
                Point3d pos = pView.Position;
                // TransformBy() returns a transformed copy
                pos = pos.TransformBy(pView.WorldToDeviceMatrix);
                int vx = (int)pos.X;
                int vy = (int)pos.Y;
                vx = e.X - vx;
                vy = e.Y - vy;
                // we move point of view to the mouse location, to create an illusion of scrolling in/out there
                dolly(pView, -vx, -vy);
                // note that we essentially ignore delta value (sign is enough for illustrative purposes)
                pView.Zoom(e.Delta > 0 ? 1.0 / 0.9 : 0.9);
                dolly(pView, vx, vy);
                MoveSensors();
                //
                Invalidate();
            }
        }
        private bool ChangePaper(string path)
        {
            if (lm != null)
            {
                lm.LayoutSwitched -= new Teigha.DatabaseServices.LayoutEventHandler(reinitGraphDevice);
                HostApplicationServices.WorkingDatabase = null;
                lm = null;
            }

            if (path == null)
            {
                splitContainer1.Panel2.Enabled = false;
                if (database != null)
                {
                    database.Dispose();
                    database = null;
                }
                helperDevice = null;
                Invalidate();
                return true;
            }

            bool bLoaded = true;
            database = new Database(false, false);
        
            if (path.ToLower().LastIndexOf(".dwg")>0)
            {
                try
                {
                    database.ReadDwgFile(path, FileOpenMode.OpenForReadAndAllShare, false, "");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    bLoaded = false;
                }
            }
            else if (path.ToLower().LastIndexOf(".dxf") > 0)
            {
                try
                {
                    database.DxfIn(path, "");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    bLoaded = false;
                }
            }

            if (bLoaded)
            {
                HostApplicationServices.WorkingDatabase = database;
                lm = LayoutManager.Current;
                lm.LayoutSwitched += new Teigha.DatabaseServices.LayoutEventHandler(reinitGraphDevice);
                String str = HostApplicationServices.Current.FontMapFileName;

                //menuStrip.

                splitContainer1.Panel2.Enabled = true;
                //this.Text = String.Format("OdViewExMgd - [{0}]", openFileDialog.SafeFileName);

                paperNow = Path.GetFileName(path);

                initializeGraphics();
                Invalidate();
            }
            return bLoaded;
        }

        void initializeGraphics()
        {
            try
            {
                graphics = Graphics.FromHwnd(splitContainer1.Panel2.Handle);
                // load some predefined rendering module (may be also "WinDirectX" or "WinOpenGL")
                using (GsModule gsModule = (GsModule)SystemObjects.DynamicLinker.LoadModule("WinGDI.txv", false, true))
                {
                    // create graphics device
                    using (Teigha.GraphicsSystem.Device graphichsDevice = gsModule.CreateDevice())
                    {
                        // setup device properties
                        using (Dictionary props = graphichsDevice.Properties)
                        {
                            if (props.Contains("WindowHWND")) // Check if property is supported
                                props.AtPut("WindowHWND", new RxVariant((Int32)splitContainer1.Panel2.Handle)); // hWnd necessary for DirectX device
                            if (props.Contains("WindowHDC")) // Check if property is supported
                                props.AtPut("WindowHDC", new RxVariant(graphics.GetHdc())); // hWindowDC necessary for Bitmap device
                            if (props.Contains("DoubleBufferEnabled")) // Check if property is supported
                                props.AtPut("DoubleBufferEnabled", new RxVariant(true));
                            if (props.Contains("EnableSoftwareHLR")) // Check if property is supported
                                props.AtPut("EnableSoftwareHLR", new RxVariant(true));
                            if (props.Contains("DiscardBackFaces")) // Check if property is supported
                                props.AtPut("DiscardBackFaces", new RxVariant(true));
                        }
                        // setup paperspace viewports or tiles
                        ContextForDbDatabase ctx = new ContextForDbDatabase(database);
                        ctx.UseGsModel = true;

                        helperDevice = LayoutHelperDevice.SetupActiveLayoutViews(graphichsDevice, ctx);
                        //          Aux.preparePlotstyles(database, ctx);
                        //       gripManager.init(helperDevice, helperDevice.Model, database);
                        //helperDevice.ActiveView.Mode = Teigha.GraphicsSystem.RenderMode.HiddenLine;
                    }
                }
                // set palette
                helperDevice.SetLogicalPalette(Device.DarkPalette);
                // set output extents
                resize();

                helperDevice.Model.Invalidate(InvalidationHint.kInvalidateAll);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (helperDevice != null)
            {
                try
                {
                    helperDevice.Update();
                }
                catch (System.Exception ex)
                {
                    graphics.DrawString(ex.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), new PointF(150.0F, 150.0F));
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //       if (selRect != null)
            //         helperDevice.ActiveView.Erase(selRect);
            //       selRect = null;

            //       gripManager.uninit();
            //       gripManager = null;
            if (graphics != null)
                graphics.Dispose();
            if (helperDevice != null)
                helperDevice.Dispose();
            if (database != null)
                database.Dispose();
            //dd.Dispose();
        }
        void resize()
        {
            if (helperDevice != null)
            {
                Rectangle r = splitContainer1.Panel2.Bounds;
                r.Offset(-splitContainer1.Panel2.Location.X, -splitContainer1.Panel2.Location.Y);
                // HDC assigned to the device corresponds to the whole client area of the panel
                helperDevice.OnSize(r);
                Invalidate();
            }
            ReplaceSensors();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            resize();
        }


        private void reinitGraphDevice(object sender, Teigha.DatabaseServices.LayoutEventArgs e)
        {
            helperDevice.Dispose();
            graphics.Dispose();
            initializeGraphics();
        }

        private void ZoomWindow(Point3d pt1, Point3d pt2)
        {
            using (Teigha.GraphicsSystem.View pView = helperDevice.ActiveView)
            {
                using (AbstractViewPE pVpPE = new AbstractViewPE(pView))
                {
                    pt1 = pt1.TransformBy(pVpPE.WorldToEye);
                    pt2 = pt2.TransformBy(pVpPE.WorldToEye);
                    Vector3d eyeVec = pt2 - pt1;

                    if (((eyeVec.X < -1E-10) || (eyeVec.X > 1E-10)) && ((eyeVec.Y < -1E-10) || (eyeVec.Y > 1E-10)))
                    {
                        Point3d newPos = pt1 + eyeVec / 2.0;
                        pView.Dolly(newPos.GetAsVector());
                        double wf = pView.FieldWidth / Math.Abs(eyeVec.X);
                        double hf = pView.FieldHeight / Math.Abs(eyeVec.Y);
                        pView.Zoom(wf < hf ? wf : hf);
                        Invalidate();
                    }
                }
            }
        }

        private void zoomWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bZoomWindow = 0;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (bZoomWindow > -1 && bZoomWindow < 2)
            {
                if (bZoomWindow == 1)
                {
                    bZoomWindow = -1;
                    ZoomWindow(firstCornerPoint, toEyeToWorld(e.X, e.Y));
                }
                if (bZoomWindow == 0)
                {
                    firstCornerPoint = toEyeToWorld(e.X, e.Y);
                    bZoomWindow = 1;
                }
            }
        }


        bool newRegApp(Database db, string regAppName)
        {
            using (RegAppTable pRegAppTable = (RegAppTable)db.RegAppTableId.GetObject(Teigha.DatabaseServices.OpenMode.ForWrite))
            {
                if (!pRegAppTable.Has(regAppName))
                {
                    using (RegAppTableRecord pRegApp = new RegAppTableRecord())
                    {
                        pRegApp.Name = regAppName;
                        pRegAppTable.Add(pRegApp);
                    }
                    return true;
                }
            }
            return false;
        }

        private void PlaceForm_Load(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            RefreshPaper();
            RefreshTree();
            ReplaceSensors();
        }

        private void splitContainer1_Panel2_DragDrop(object sender, DragEventArgs e)
        {
            object[] datas = (object[])e.Data.GetData(typeof(object[]));
            Site st = (Site)datas[1];
            Sensor ss = (Sensor)datas[0];
            Point ptClient = splitContainer1.Panel2.PointToClient(new Point(e.X, e.Y));
            if (ss != null)
            {
                Point3d pt3d = toEyeToWorld(ptClient.X,ptClient.Y);
                ss.paper = paperNow;
                ss.positionX = pt3d.X;
                ss.positionY = pt3d.Y;
                ss.positionZ = pt3d.Z;
                ReplaceSensor(st.num,ss);
            }
        }

        public void ReplaceSensors()
        {
            Site st;
            int i = 0;
            while (sites.GetAt(i++,out st))
            {
                Sensor ss;
                int j = 0;
                while(st.GetAt(j++,out ss))
                {
                    ReplaceSensor(st.num,ss);
                }
            }
        }

        public void MoveSensors()
        {
            foreach (KeyValuePair<Sensor, FormSensorIcon> kvp in icons)
            {
                kvp.Value.Location = ToClient(new Point3d(kvp.Key.positionX,
                    kvp.Key.positionY,
                    kvp.Key.positionZ));
            }
        }

        public void ReplaceSensor(uint siteNum,Sensor ss)
        {
            if (ss.paper==null)
            {
                return;
            }
            FormSensorIcon fsi;
            if (ss.paper!=paperNow)
            {
                
                if (icons.TryGetValue(ss, out fsi))
                {
                    this.Controls.Remove(fsi);
                    icons.Remove(ss);
                    fsi.Dispose();
                }
                return;
            }
            Point pt = ToClient(new Point3d(ss.positionX,ss.positionY,ss.positionZ));
            if (!icons.TryGetValue(ss,out fsi))
            {
                fsi = new FormSensorIcon(siteNum,ss);
                icons.Add(ss, fsi);
                fsi.SetText(ss.name);
            }
            try
            {
                this.splitContainer1.Panel2.Controls.Add(fsi);

            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
            fsi.Location = pt;
            fsi.Show();
        }

        private void treeViewSensors_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left )
            {
                TreeNode tn = treeViewSensors.GetNodeAt(e.X, e.Y);
                if (null != tn && null != tn.Tag && tn.Tag.GetType() == typeof(Sensor))
                {
                    TreeNode tnP = tn.Parent;
                    treeViewSensors.SelectedNode = tn;
                    if (bEdit)
                    {
                        treeViewSensors.DoDragDrop(new object[]{tn.Tag,tnP.Tag}, DragDropEffects.Move);
                    }
                }
            }
        }

        private void RefreshTree()
        {
            treeViewSensors.Nodes[0].Nodes.Clear();
            Site st;
            int i = 0;
            while (sites.GetAt(i++, out st))
            {
                TreeNode tn = treeViewSensors.Nodes[0].Nodes.Add(st.num.ToString(), "(" + st.num.ToString() + ")" + st.name);
                tn.Tag = st;
                Sensor ss;
                int j = 0;
                while (st.GetAt(j++, out ss))
                {
                    TreeNode tns = tn.Nodes.Add(ss.num.ToString(), "(" + ss.num.ToString() + ")" + ss.name);
                    tns.Tag = ss;
                }
            }
            treeViewSensors.ExpandAll();
        }

        private void splitContainer1_Panel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(object[]))!=null)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private Sites sites;
        private string paperNow;
        private Dictionary<Sensor, FormSensorIcon> icons = new Dictionary<Sensor, FormSensorIcon>();
        private bool bEdit = false;

        private void splitContainer1_Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            splitContainer1.Panel2.Focus();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            //file_open_handler(this, new EventArgs());
            if (DialogResult.OK == openFileDialog.ShowDialog(this))
            {
                if (!Directory.Exists("图纸"))
                {
                    Directory.CreateDirectory("图纸");
                }
                string name = Microsoft.VisualBasic.Interaction.InputBox("请输入图纸名", "导入图纸","", -1, -1);
                while(File.Exists("图纸\\"+name))
                {
                    name = Microsoft.VisualBasic.Interaction.InputBox("请输入图纸名", "导入图纸","", -1, -1);
                }
                try
                {
                    File.Copy(openFileDialog.FileName, "图纸\\" + name + Path.GetExtension(openFileDialog.FileName), false);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("导入图纸失败：" + ex.Message);
                }
                RefreshPaper();
            }
        }

        public void RefreshPaper()
        {
            listBoxPaper.Items.Clear();
             DirectoryInfo dif = new DirectoryInfo("图纸");
            if (!dif.Exists)
            {
                dif.Create();
            }

            FileInfo[] files = dif.GetFiles();

            foreach (FileInfo file in files)
            {
                listBoxPaper.Items.Add(file.Name);
            }
            ChangePaper(null);
            paperNow = "";
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确定要删除吗？","删除图纸",MessageBoxButtons.OKCancel))
            {
                string path = "图纸\\" + (string)listBoxPaper.SelectedItem;
                Reset();
                File.Delete(path);
                RefreshPaper();
            }
            
        }

        private void listBoxPaper_SelectedIndexChanged(object sender, EventArgs e)
        {
             ChangePaper( "图纸\\"+(string)listBoxPaper.SelectedItem);
             ReplaceSensors();
        }

        private void treeViewSensors_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag.GetType() != typeof(Sensor))
            {
                return;
            }
            Site st = (Site)e.Node.Parent.Tag;
            Sensor ss = (Sensor)e.Node.Tag;
            if (ss.paper == null)
            {
                return;
            }
            if (!listBoxPaper.Items.Contains(ss.paper))
            {
                return;
            }
            listBoxPaper.SelectedItem = ss.paper;
        }
    }
}
