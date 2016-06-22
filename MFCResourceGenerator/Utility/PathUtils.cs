using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MFCResourceGenerator.Utility
{
    public class PathUtils
    {
        // http://stackoverflow.com/questions/13266756/absolute-to-relative-path
        public static string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath);
            return referenceUri.MakeRelativeUri(fileUri).ToString();
        }
    }
}
