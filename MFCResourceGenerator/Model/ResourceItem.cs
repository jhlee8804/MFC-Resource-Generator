using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MFCResourceGenerator.Model
{
    public class ResourceItem
    {
        public ResourceItem(string path)
        {
            FileName = Path.GetFileNameWithoutExtension(path);
            FilePath = path;
            Type = Path.GetExtension(path).Replace(".", string.Empty).ToUpper();

            Debug.Assert(!string.IsNullOrEmpty(FileName));
            Debug.Assert(!string.IsNullOrEmpty(FilePath));
            Debug.Assert(!string.IsNullOrEmpty(Type));
        }

        /// <summary>
        /// File name of resource file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Relative path of resource file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Resource type (cf. PNG, BITMAP)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// "#define IDB_DIR1_DIR2_DIR3_FILENAME
        /// </summary>
        public string DefinedResourceIdString
        {
            get
            {
                string text = "#define IDB_";

                string id = string.Empty;
                var segments = FilePath.Split(Path.AltDirectorySeparatorChar).ToList();

                // remove first dir name
                if (segments.Count > 2)
                    segments.RemoveAt(0);

                id = string.Join("_", segments.ToArray());
                id = id.ToUpper();

                // remove file extension
                id = id.Replace("." + Type, string.Empty);

                text += id;
                return text;
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
                text += "\t\t" + FilePath;

                return text;
            }
        }

        public override string ToString()
        {
            return "File: " + FilePath;
        }
    }
}
