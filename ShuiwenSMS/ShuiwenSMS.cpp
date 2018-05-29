// ShuiwenSMS.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <Windows.h>
#include <atlstr.h>
#include <atltrace.h>
#include <set>
using namespace std;

static LPCTSTR RECV_NAME="允许接收";

LPCSTR SECTION_NAME="COM";
LPCSTR BAUD_NAME="BAUD"; 
LPCSTR PARITY_NAME="PARITY";
LPCSTR STOP_NAME="STOP";
LPCSTR DATA_NAME="DATA";
LPCSTR PORT_NAME="PORT";

static  UINT DATA[4]={5,6,7,8};			//数据位数组
static	UINT STOP[2]={ONESTOPBIT,TWOSTOPBITS};					//停止位数组
static	UINT PARITY[5]={NOPARITY,ODDPARITY,EVENPARITY,MARKPARITY,SPACEPARITY};	//校验位数组

static set<CString> g_filterNum;

CString GetExeDir()
{
	TCHAR CurrentPath[MAX_PATH]={0};
	GetModuleFileName(NULL,CurrentPath,MAX_PATH-1);
	for (int i= strlen(CurrentPath)-1;i>0;--i)
	{
		if (CurrentPath[i]=='\\')
		{
			CurrentPath[i]='\0';
			break;
		}
	}
	return CString(CurrentPath);
}

static BOOL SendToCom(HANDLE hCom, LPCVOID pByte, int nCount,LPCTSTR pszRight,LPCTSTR pszWrong)
{
	DWORD dwReturn;
	ATLTRACE("SEND:%s\n",pByte);
	if (!WriteFile(hCom,pByte,nCount,&dwReturn,NULL))
	{
		return FALSE;
	}
	Sleep(1);
	CString strTmp;
	char ch[1024] = {0};
	int nWait = 1;
	while(ReadFile(hCom,ch,1023,&dwReturn,NULL))
	{
		ch[dwReturn] = 0;

		ATLTRACE("RECV:%s\n",ch);
		strTmp += ch;
		if (pszRight && strTmp.Find(pszRight)>=0)
		{
			PurgeComm(hCom,PURGE_RXABORT|PURGE_RXCLEAR);
			return TRUE;
		}
		else if (pszWrong && strTmp.Find(pszWrong)>=0)
		{
			PurgeComm(hCom,PURGE_RXABORT|PURGE_RXCLEAR);
			return FALSE;
		}
		else if ((pszRight || pszWrong) && 0 < nWait-- )
		{
			continue;
		}
		else
		{
			break;
		}
	}

	ATLTRACE("%s  BREAK!!!\n",strTmp.operator LPCTSTR());


	return TRUE;
}

void SplitStr(const char* src,set<CString>& result,char split)
{
	const char* pLast = src;
	while(*src)
	{
		if ((*(src+1) == split || *(src+1)=='\0') && src - pLast>0)
		{
			CString subStr;
			subStr.Append(pLast,src - pLast+1);
			result.insert(subStr);
			pLast = src+2;
		}
		++src;
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	///////////////////////////////////////////
	//读配置文件
	///////////////////////////////////////////
	char buff[4096] = {0};
	CString iniPath = GetExeDir()+"\\sms.ini";
	::GetPrivateProfileString(SECTION_NAME,RECV_NAME,"",buff,4095,iniPath);
	SplitStr(buff,g_filterNum,',');


	CHAR ch[256]={0};
	::GetPrivateProfileStringA("COM","PORT","",ch,1023,iniPath);
	CString strCom = ch;
	::GetPrivateProfileString(SECTION_NAME,BAUD_NAME,"9600",ch,255,iniPath);
	int nBaud = atoi(ch);
	int nData = ::GetPrivateProfileInt(SECTION_NAME, DATA_NAME, 3, iniPath);
	int nParity = ::GetPrivateProfileInt(SECTION_NAME, PARITY_NAME, 0, iniPath);
	int nStop = ::GetPrivateProfileInt(SECTION_NAME, STOP_NAME,0,iniPath);
	int nRecvBoxLen =::GetPrivateProfileInt("SMS", "RecvBoxLen",75,iniPath);
	//////////////////////////////////////////////////////////////////////////
	///打开串口
	//////////////////////////////////////////////////////////////////////////
	DCB dcb;
	HANDLE hCom;
	BOOL fSuccess;
	hCom = CreateFileA( CString("\\\\.\\") + strCom,
		GENERIC_READ | GENERIC_WRITE,
		0,    // must be opened with exclusive-access
		NULL, // default security attributes
		OPEN_EXISTING, // must use OPEN_EXISTING
		NULL,//FILE_FLAG_OVERLAPPED,    // not overlapped I/O
		NULL  // hTemplate must be NULL for comm devices
		);

	if (hCom == INVALID_HANDLE_VALUE) 
	{
		// Handle the error.
		printf("ERR----串口COM:%s打开失败\n",strCom.operator LPCTSTR());
		return 1;
	}

	// Build on the current configuration, and skip setting the size
	// of the input and output buffers with SetupComm.

	ZeroMemory(&dcb, sizeof(DCB));
	dcb.DCBlength = sizeof(DCB);
	fSuccess = GetCommState(hCom, &dcb);

	// 	if (!fSuccess) 
	// 	{
	// 		// Handle the error
	// 		return FALSE;
	// 	}

	dcb.BaudRate = nBaud;     // set the baud rate
	dcb.ByteSize = DATA[nData];             // data size, xmit, and rcv
	dcb.Parity = PARITY[nParity];        // no parity bit
	dcb.StopBits = STOP[nStop];    // one stop bit

	fSuccess = SetCommState(hCom, &dcb);

	COMMTIMEOUTS cto;
	cto.ReadIntervalTimeout = 100;
	cto.ReadTotalTimeoutMultiplier = 200;//接收超时
	cto.ReadTotalTimeoutConstant =1000;
	cto.WriteTotalTimeoutConstant = 1000;//发送超时
	cto.WriteTotalTimeoutMultiplier = 0;
	fSuccess = SetCommTimeouts(hCom,&cto);

	COMSTAT ComStat;
	DWORD dwErrorFlags;
	ClearCommError(hCom,&dwErrorFlags,&ComStat);
/////////////////////////////////////////////////////////
//读取短信
////////////////////////////////////////////////////////
	char dataSend[128] = {0};
	char dataRecv[4096] = {0};
	CString at = "";

	DWORD dwWritten = 0;
	if(1)
	{
		at = "AT&F\r\n";
		if(!SendToCom(hCom,at.operator LPCTSTR(),at.GetLength(),"OK","ERROR"))
		{
			printf("ERR----发送AT&F返回失败");
			return 1;
		}
		at = "ATE0\r\n";
		if(!SendToCom(hCom,at.operator LPCTSTR(),at.GetLength(),"OK","ERROR"))
		{
			printf("ERR----发送ATE0返回失败");
			return 1;
		}
		at = "AT+CMGF=0\r\n";
		if(!SendToCom(hCom,at.operator LPCTSTR(),at.GetLength(),"OK","ERROR"))
		{
			printf("ERR----发送AT+CMGF=0返回失败");
			return 1;
		}
		for (int idx=0;idx<nRecvBoxLen;++idx)
		{
			CString msg;
			CString strTime = "20";
			int nLen = sprintf(dataSend,"AT+CMGR=%d\r\n",idx+1);
			if (!WriteFile(hCom,dataSend,nLen,&dwWritten,NULL))
			{
				break;
			}
			if (!ReadFile(hCom,dataRecv,4095,&dwWritten,NULL))
			{
				break;
			}
			
			dataRecv[dwWritten] = 0;
			ATLTRACE(dataRecv);
			at = dataRecv;
			at.MakeUpper();
			
			int start = at.Find(',');
			if (start < 0)
			{
				continue;
			}
			start = at.Find('\n', start + 1);
			int end = at.Find("OK");
			if (end < 0 || end - start < 10 )
			{
				continue;
			}
			end  = at.Find('\n',start+1);
			CString strPDU = at.Mid(start,end-start);
			strPDU.Remove('\r');
			strPDU.Remove('\n');
			end = strPDU.GetLength();
			UINT n=0;
			CString tmp = strPDU.Left(2);
			sscanf(tmp.operator LPCTSTR(),"%X",&n);
			start = 2*(n+2);
			tmp = strPDU.Mid(start,2);
			sscanf(tmp.operator LPCTSTR(),"%X",&n);
			int count = n+n%2;
			CString num;
			start +=4;
			for (int i = start; i< start + count; i+=2)
			{
				num += strPDU[i+1];
				num += strPDU[i];
			}
			num.Remove('F');
			start += count+2;
			tmp = strPDU.Mid(start,2);
			int codec;
			sscanf(tmp.operator LPCTSTR(),"%X",&codec);
			
			for (int i=start+2;i<start+2+12;i+=2 )
			{
				strTime += strPDU[i+1];
				strTime += strPDU[i];
			}

			start += 8*2;
			tmp = strPDU.Mid(start,2);
			sscanf(tmp.operator LPCTSTR(),"%X",&n);
			start+=2;
			if ((codec&0x0C) == 0x08)//unicode编码
			{
				start = end - n*2;
				CStringW msgW;
				int l,h;
				for (int i=start;i<end;i+=4)
				{
					tmp = strPDU.Mid(i,2);
					sscanf(tmp.operator LPCTSTR(),"%X",&h);
					tmp = strPDU.Mid(i+2,2);
					sscanf(tmp.operator LPCTSTR(),"%X",&l);
					msgW+= (wchar_t)(l | (h<<8));
				}
				msg = msgW;	 
			}
			else if ((codec&0x0C) == 0)//7bit码
			{
				start = end - (n*7/8 + (n*7%8?1:0))*2;
				BYTE before = 0;
				int i7 = 0;
				for(int i=start;i<end;i+=2)
				{
					tmp = strPDU.Mid(i,2);
					sscanf(tmp.operator LPCTSTR(),"%X",&n);
					int move = (i7++)%8;
					char ch =  (char)(((n << move)| (before>>(8 -move) )) & 0x7F);
					msg += ch;
					if (move == 7 )
					{
						msg += (char)(n & 0x7F);
						++i7;
					}
					before = n;
				}
			}
			else if ((codec&0x0C) == 0x04)//8bit码
			{
				start = end - 2*n;
				for(int i= start;i<end;i+=2)
				{
					tmp = strPDU.Mid(i,2);
					sscanf(tmp.operator LPCTSTR(),"%X",&n);
					msg += (char)n;
				}
				
			}
			printf("MSG----%s----%s|\r\n",msg.operator LPCSTR(),strTime.operator LPCSTR());
			at.Format("AT+CMGD=%d\r\n",idx+1);
			SendToCom(hCom,at.operator LPCTSTR(),at.GetLength(),"OK","ERROR");
		}
	}
}


