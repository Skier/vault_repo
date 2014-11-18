using System;
using System.Text;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;
using System.IO;
 
namespace DPI.Maint
{
	public class TextUtil
	{
		public static string[] ReadText(string path)
		{
			ArrayList ar = new ArrayList();
			TextReader tr = new StreamReader(path);
			
			string s;
			while ((s = tr.ReadLine()) != null)
				ar.Add(s);
			
			tr.Close();

			string[] doc = new string[ar.Count];
			ar.CopyTo(doc);
			return doc;
		}

		public static void WriteText(string[] text, string path)
		{
			StreamWriter sw = new StreamWriter(GetFS(path));

			for (int i = 0; i < text.Length; i++)
				sw.WriteLine(text[i]);
			
			sw.Flush();
			sw.Close();
		}
		public static void WriteTextOverride(string[] text, string path)
		{
			StreamWriter sw = new StreamWriter(GetFSOverwrite(path));

			for (int i = 0; i < text.Length; i++)
				sw.WriteLine(text[i]);
			
			sw.Flush();
			sw.Close();
		}
		public static void DeleteFile(string path)
		{
			if (IsExists(path))
				File.Delete(path);			
		}
		public static void TruncateFile(string path)
		{
			FileStream fs = null;
			if (IsExists(path))
			{
				fs = new FileStream(path, FileMode.Truncate);
				fs.Close();
			}
			
		}
		
		public static void WriteText(string text, string path)
		{
			WriteText(new string[] { text }, path);
		}
		public static void OverWriteText(string  text, string path)
		{   
			StreamWriter sw = new StreamWriter(GetFSOverwrite(path));
			
			string[] stringConvert = new  string[] {text};
			for (int i = 0; i < stringConvert.Length; i++)
				sw.WriteLine(stringConvert[i]);
			
			sw.Flush();
			sw.Close();
		}

		public static bool IsExists(string path)
		{
			return new FileInfo(path).Exists;
		}
		
		public static FileStream GetFS(string path)
		{
			if (IsExists(path))
				return File.Open(path, FileMode.Append);
			return File.Create(path);
		}

		public static FileStream GetFSOverwrite(string path)
		{
			return File.Create(path);
		}
		public static string Folder(string path)
		{		
			if (Directory.Exists(path)) 
				return path;

			
				Directory.CreateDirectory(path).ToString();
				return path;
		}
	}
}