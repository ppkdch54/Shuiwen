using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace ShuiwenLib
{
    public delegate double FumulaDelegate(double input);
    public class FormulaBuilder
    {
        private static uint nID;
        public static FumulaDelegate TryCompile(string str,out string strErr)
        {
            strErr = "";
            string clsName = "Formula" + ((++nID).ToString());
            // 1.CSharpCodePrivoder
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

            // 2.ICodeComplier
            ICodeCompiler objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();

            // 3.CompilerParameters
            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;

            // 4.CompilerResults
            CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(objCompilerParameters, GenerateCode(str,clsName));

            if (cr.Errors.HasErrors)
            {
                foreach (CompilerError err in cr.Errors)
                {
                    strErr += err;
                }
                return null;
            }
            else
            {
                // 通过反射，调用HelloWorld的实例
                Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType("ShuiwenLib."+clsName);
                MethodInfo mi = t.GetMethod("OutPut");
                Delegate d = Delegate.CreateDelegate(typeof(FumulaDelegate), mi);

                return (FumulaDelegate)d;
            }
        }

        static string GenerateCode(string formula,string className)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace ShuiwenLib");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("    public class " + className);
            sb.Append(Environment.NewLine);
            sb.Append("    {");
            sb.Append(Environment.NewLine);
            sb.Append("        public static double OutPut(double data)");
            sb.Append(Environment.NewLine);
            sb.Append("        {");
            sb.Append(Environment.NewLine);
            sb.Append("             return " + formula + ";");
            sb.Append(Environment.NewLine);
            sb.Append("        }");
            sb.Append(Environment.NewLine);
            sb.Append("    }");
            sb.Append(Environment.NewLine);
            sb.Append("}");

            string code = sb.ToString();
            Console.WriteLine(code);
            Console.WriteLine();

            return code;
        }
    }
}
