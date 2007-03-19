using System;
using System.Runtime.InteropServices;

namespace PTM.View
{
	internal sealed class MemoryMappedFile : IDisposable
	{
		private const int FILE_MAP_WRITE = 0x2;
		private const int FILE_MAP_READ = 0x0004;


		[DllImport("kernel32.dll", EntryPoint="OpenFileMapping", SetLastError=true, CharSet=CharSet.Auto)]
		private static extern IntPtr OpenFileMapping(int dwDesiredAccess, bool bInheritHandle, String lpName);

		[DllImport("Kernel32.dll", EntryPoint="CreateFileMapping", SetLastError=true, CharSet=CharSet.Auto)]
		private static extern IntPtr CreateFileMapping(uint hFile, IntPtr lpAttributes, uint flProtect, uint dwMaximumSizeHigh,
		                                               uint dwMaximumSizeLow, string lpName);

		[DllImport("Kernel32.dll")]
		private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh,
		                                           uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

		[DllImport("Kernel32.dll", EntryPoint="UnmapViewOfFile", SetLastError=true, CharSet=CharSet.Auto)]
		[return : MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		[DllImport("kernel32.dll", EntryPoint="CloseHandle", SetLastError=true, CharSet=CharSet.Auto)]
		[return : MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(uint hHandle);

//		[DllImport("kernel32.dll", EntryPoint="GetLastError", SetLastError=true, CharSet=CharSet.Auto)]
//		private static extern uint GetLastError();

		private IntPtr memoryFileHandle;

		[FlagsAttribute]
		internal enum FileAccess : int
		{
			ReadOnly = 2,
			ReadWrite = 4
		} //FileAccess

		private MemoryMappedFile(IntPtr memoryFileHandle)
		{
			this.memoryFileHandle = memoryFileHandle;
		} //MemoryMappedFile

		internal static MemoryMappedFile CreateMMF(string fileName, FileAccess access, int size)
		{
			if (size < 0)
			{
				throw new ArgumentException("The size parameter should be a number greater than Zero.");
			} //if

			IntPtr memoryFileHandle = CreateFileMapping(0xFFFFFFFF, IntPtr.Zero, (uint) access, 0, (uint) size, fileName);
			if (memoryFileHandle == IntPtr.Zero)
			{
				throw new SharedMemoryException("Creating Shared Memory failed.");
			} //if

			return new MemoryMappedFile(memoryFileHandle);
		} //CreateMMF

		internal static IntPtr ReadHandle(string fileName)
		{
			IntPtr mappedFileHandle = OpenFileMapping((int) FileAccess.ReadWrite, false, fileName);
			if (mappedFileHandle == IntPtr.Zero)
			{
				throw new SharedMemoryException("Opening the Shared Memory for Read failed.");
			} //if

			IntPtr mappedViewHandle = MapViewOfFile(mappedFileHandle, (uint) FILE_MAP_READ, 0, 0, 8);
			if (mappedViewHandle == IntPtr.Zero)
			{
				throw new SharedMemoryException("Creating a view of Shared Memory failed.");
			} //if

			IntPtr windowHandle = Marshal.ReadIntPtr(mappedViewHandle);
			if (windowHandle == IntPtr.Zero)
			{
				throw new ArgumentException("Reading from the specified address in  Shared Memory failed.");
			} //if

			UnmapViewOfFile(mappedViewHandle);
			CloseHandle((uint) mappedFileHandle);
			return windowHandle;
		} //ReadHandle

		internal void WriteHandle(IntPtr windowHandle)
		{
			IntPtr mappedViewHandle = MapViewOfFile(memoryFileHandle, (uint) FILE_MAP_WRITE, 0, 0, 8);
			if (mappedViewHandle == IntPtr.Zero)
			{
				throw new SharedMemoryException("Creating a view of Shared Memory failed.");
			} //if

			Marshal.WriteIntPtr(mappedViewHandle, windowHandle);

			UnmapViewOfFile(mappedViewHandle);
			CloseHandle((uint) mappedViewHandle);
		} //WriteHandle

		#region IDisposable Member

		public void Dispose()
		{
			if (memoryFileHandle != IntPtr.Zero)
			{
				if (CloseHandle((uint) memoryFileHandle))
					memoryFileHandle = IntPtr.Zero;
			}
		}

		#endregion
	} //end of MemoryMappedFile class

	[Serializable]
	internal class SharedMemoryException : Exception
	{
		internal SharedMemoryException()
		{
		} //SharedMemoryException

		internal SharedMemoryException(string message) : base(message)
		{
		} //SharedMemoryException

		internal SharedMemoryException(string message, Exception inner) : base(message, inner)
		{
		} //SharedMemoryException
	} //end of SharedMemoryException class
} //end of namespace