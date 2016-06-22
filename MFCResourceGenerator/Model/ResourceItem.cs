using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using MFCResourceGenerator.Configuration;
using MFCResourceGenerator.Utility;

namespace MFCResourceGenerator.Model
{
    public class ResourceItem
    {
        private Options _options;

        public ResourceItem(string filePath, Options options)
        {
            Debug.Assert(!string.IsNullOrEmpty(filePath));
            Debug.Assert(options != null);

            _options = options;

            FileName = Path.GetFileNameWithoutExtension(filePath);
            Type = Path.GetExtension(filePath).Replace(".", string.Empty).ToUpper();
            FileRelativePath = PathUtils.MakeRelative(filePath, options.WorkingPath);
            FileFullPath = Path.GetFullPath(filePath);
        }

        /// <summary>
        /// File name of resource file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Resource type (cf. PNG, BITMAP)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Relative path of resource file.
        /// </summary>
        public string FileRelativePath { get; set; }

        /// <summary>
        /// Full path of resource file
        /// </summary>
        public string FileFullPath { get; set; }

        /// <summary>
        /// "#define IDB_DIR1_DIR2_DIR3_FILENAME
        /// </summary>
        public string DefinedResourceIdString
        {
            get
            {
                string text = "#define IDB_";

                string temp = string.Empty;
                {
                    var path = PathUtils.MakeRelative(FileFullPath, _options.ResourcePath);
                    var segments = path.Split(Path.AltDirectorySeparatorChar).ToList();

                    temp = string.Join("_", segments.ToArray());
                    temp = temp.ToUpper();

                    // remove file extension
                    temp = temp.Replace("." + Type, string.Empty);
                }

                text += temp;
                return text.Trim();
            }
        }

        /// <summary>
        /// "IDB_DIR1_DIR2_DIR3_FILENAME    TYPE    PATH
        /// </summary>
        public string DefinedRCPathString
        {
            get
            {
                string text = DefinedResourceIdString;

                // remove "#define"
                text = text.Replace("#define", string.Empty);

                // add resource type
                text += "\t\t" + Type.ToUpper();

                // add file path
                text += "\t\t" + "\"" + FileRelativePath + "\"";

                return text.Trim();
            }
        }

        public override string ToString()
        {
            return "File: " + FileRelativePath;
        }
    }
}
