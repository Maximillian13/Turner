// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2015 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// Changes to this file will be reverted when you update Steamworks.NET

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks {
	public class InteropHelp {
		public static void TestIfPlatformSupported() {
#if !UNITY_EDITOR && !UNITY_STANDALONE_WIN && !UNITY_STANDALONE_LINUX && !UNITY_STANDALONE_OSX && !STEAMWORKS_WIN && !STEAMWORKS_LIN_OSX
			throw new System.InvalidOperationException("Steamworks functions can only be called on platforms that Steam is available on.");
#endif
		}

		public static void TestIfAvailableClient() {
			TestIfPlatformSupported();
			if (NativeMethods.SteamClient() == System.IntPtr.Zero) {
				throw new System.InvalidOperationException("Steamworks is not initialized.");
			}
		}

		public static void TestIfAvailableGameServer() {
			TestIfPlatformSupported();
			if (NativeMethods.SteamClientGameServer() == System.IntPtr.Zero) {
				throw new System.InvalidOperationException("Steamworks is not initialized.");
			}
		}
		
		// This continues to exist for both 'out string' and strings returned by Steamworks functions.
		public static string PtrToStringUTF8(IntPtr nativeUtf8) {
			if (nativeUtf8 == IntPtr.Zero) {
				return string.Empty;
			}

			int len = 0;

			while (Marshal.ReadByte(nativeUtf8, len) != 0) {
				++len;
			}

			if (len == 0) {
				return string.Empty;
			}

			byte[] buffer = new byte[len];
			Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
			return Encoding.UTF8.GetString(buffer);
		}

		// This is for 'const char *' arguments which we need to ensure do not get GC'd while Steam is using them.
		// We can't use an ICustomMarshaler because Unity crashes when a string between 96 and 127 characters long is defined/initialized at the top of class scope...
		public class UTF8StringHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid {
			public UTF8StringHandle(string str)
				: base(true) {
				if (str == null) {
					SetHandle(IntPtr.Zero);
					return;
				}

				byte[] strbuf = new byte[Encoding.UTF8.GetByteCount(str) + 1];
				Encoding.UTF8.GetBytes(str, 0, str.Length, strbuf, 0);
				IntPtr buffer = Marshal.AllocHGlobal(strbuf.Length);
				Marshal.Copy(strbuf, 0, buffer, strbuf.Length);

				SetHandle(buffer);
			}

			protected override bool ReleaseHandle() {
				if (!IsInvalid) {
					Marshal.FreeHGlobal(handle);
				}
				return true;
			}
		}

		
		// We can't use an ICustomMarshaler because Unity dies when MarshalManagedToNative() gets called with a generic type.
		public class SteamParamStringArray {
			// The pointer to each AllocHGlobal() string
			IntPtr[] m_Strings;
			// The pointer to the condensed version of m_Strings
			IntPtr m_ptrStrings;
			// The pointer to the StructureToPtr version of SteamParamStringArray_t that will get marshaled
			IntPtr m_pSteamParamStringArray;

			public SteamParamStringArray(System.Collections.Generic.IList<string> strings) {
				if (strings == null) {
					m_pSteamParamStringArray = IntPtr.Zero;
					return;
				}

				m_Strings = new IntPtr[strings.Count];
				for (int i = 0; i < strings.Count; ++i) {
					byte[] strbuf = new byte[Encoding.UTF8.GetByteCount(strings[i]) + 1];
					Encoding.UTF8.GetBytes(strings[i], 0, strings[i].Length, strbuf, 0);
					m_Strings[i] = Marshal.AllocHGlobal(strbuf.Length);
					Marshal.Copy(strbuf, 0, m_Strings[i], strbuf.Length);
				}

				m_ptrStrings = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * m_Strings.Length);
				SteamParamStringArray_t stringArray = new SteamParamStringArray_t() {
					m_ppStrings = m_ptrStrings,
					m_nNumStrings = m_Strings.Length
				};
				Marshal.Copy(m_Strings, 0, stringArray.m_ppStrings, m_Strings.Length);

				m_pSteamParamStringArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SteamParamStringArray_t)));
				Marshal.StructureToPtr(stringArray, m_pSteamParamStringArray, false);
			}

			~SteamParamStringArray() {
				foreach (IntPtr ptr in m_Strings) {
					Marshal.FreeHGlobal(ptr);
				}

				if (m_ptrStrings != IntPtr.Zero) {
					Marshal.FreeHGlobal(m_ptrStrings);
				}

				if (m_pSteamParamStringArray != IntPtr.Zero) {
					Marshal.FreeHGlobal(m_pSteamParamStringArray);
				}
			}

			public static implicit operator IntPtr(SteamParamStringArray that) {
				return that.m_pSteamParamStringArray;
			}
		}
	}
	
	
	// MatchMaking Key-Value Pair Marshaller
	public class MMKVPMarshaller {
		private IntPtr m_pNativeArray;
		private IntPtr m_pArrayEntries;

		public MMKVPMarshaller(MatchMakingKeyValuePair_t[] filters) {
			if (filters == null) {
				return;
			}

			int sizeOfMMKVP = Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t));

			m_pNativeArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * filters.Length);
			m_pArrayEntries = Marshal.AllocHGlobal(sizeOfMMKVP * filters.Length);
			for (int i = 0; i < filters.Length; ++i) {
				Marshal.StructureToPtr(filters[i], new IntPtr(m_pArrayEntries.ToInt64() + (i * sizeOfMMKVP)), false);
			}

			Marshal.WriteIntPtr(m_pNativeArray, m_pArrayEntries);
		}

		~MMKVPMarshaller() {
			if (m_pArrayEntries != IntPtr.Zero) {
				Marshal.FreeHGlobal(m_pArrayEntries);
			}
			if (m_pNativeArray != IntPtr.Zero) {
				Marshal.FreeHGlobal(m_pNativeArray);
			}
		}

		public static implicit operator IntPtr(MMKVPMarshaller that) {
			return that.m_pNativeArray;
		}
	}

	public class DllCheck {
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		extern static int GetModuleFileName(IntPtr hModule, StringBuilder strFullPath, int nSize);

		/// <summary>
		/// This is an optional runtime check to ensure that the dlls are the correct version. Returns false only if the steam_api.dll is found and it's the wrong size or version number.
		/// </summary>
		public static bool Test() {
			//bool ret = CheckSteamAPIDLL();
			return true;
		}

		private static bool CheckSteamAPIDLL() {
#if STEAMWORKS_WIN || (UNITY_EDITOR_WIN && UNITY_STANDALONE) || (!UNITY_EDITOR && UNITY_STANDALONE_WIN)
			string fileName;
			int fileBytes;
			if (IntPtr.Size == 4) {
				fileName = "steam_api.dll";
				fileBytes = Version.SteamAPIDLLSize;
			}
			else {
				fileName = "steam_api64.dll";
				fileBytes = Version.SteamAPI64DLLSize;
			}

			IntPtr handle = GetModuleHandle(fileName);
			if (handle == IntPtr.Zero) {
				return true;
			}

			StringBuilder filePath = new StringBuilder(256);
			GetModuleFileName(handle, filePath, filePath.Capacity);
			string file = filePath.ToString();

			// If we can not find the file we'll just skip it and let the DllNotFoundException take care of it.
			if (System.IO.File.Exists(file)) {
				System.IO.FileInfo fInfo = new System.IO.FileInfo(file);
				if (fInfo.Length != fileBytes) {
					return false;
				}

				if (System.Diagnostics.FileVersionInfo.GetVersionInfo(file).FileVersion != Version.SteamAPIDLLVersion) {
					return false;
				}
			}
#endif
			return true;
		}
	}
}
