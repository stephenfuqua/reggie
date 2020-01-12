using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace safnet.SystemAdapters
{
    /// <summary>
    /// Defines methods for interacting with the operating system
    /// </summary>
    public interface IFileAdapter
    {
        T[] DeserializeXmlFile<T>(string fileToRead);
        void SerializeXmlFile<T>(T[] collectionToSerialize, string fileToWrite);

        string OpenFileOpenDialogBox(string extension, string filter);
        string OpenFileSaveDialogBox(string extension, string filter);

        void OpenInDefaultApplication(string fileToOpen);
    }

    /// <summary>
    /// MS Windows-specific Adapter / wrapper around file open/close operations
    /// </summary>
    public class WindowsFileAdapter : IFileAdapter
    {
        /// <summary>
        /// Read an XML file into an array of objects
        /// </summary>
        /// <typeparam name="T">The data type to read</typeparam>
        /// <param name="fileToRead">Input file path and name</param>
        /// <returns>Array of T</returns>
        public T[] DeserializeXmlFile<T>(string fileToRead)
        {
            if (fileToRead == null)
            {
                throw new ArgumentNullException("fileToRead");
            }
            if (string.IsNullOrWhiteSpace(fileToRead))
            {
                throw new ArgumentException("fileToRead cannot be blank", "fileToRead");
            }

            T[] collection = null;

            XmlSerializer serializer = new XmlSerializer(typeof(T[]));

            using (XmlReader reader = XmlReader.Create(fileToRead))
            {
                collection = (T[])serializer.Deserialize(reader);
            }

            return collection;
        }

        /// <summary>
        /// Write an XML-serializable array of objects to a file
        /// </summary>
        /// <typeparam name="T">The data type to write</typeparam>
        /// <param name="collectionToSerialize">Array of objects to save</param>
        /// <param name="fileToWrite">Output file path and name</param>
        public void SerializeXmlFile<T>(T[] collectionToSerialize, string fileToWrite)
        {
            if (fileToWrite == null)
            {
                throw new ArgumentNullException("fileToWrite");
            }
            if (string.IsNullOrWhiteSpace(fileToWrite))
            {
                throw new ArgumentException("fileToWrite cannot be blank", "fileToWrite");
            }
            if (collectionToSerialize == null)
            {
                throw new ArgumentNullException("collectionToSerialize");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T[]));

            using (XmlWriter writer = XmlWriter.Create(fileToWrite))
            {
                serializer.Serialize(writer, collectionToSerialize);
            }
        }


        /// <summary>
        /// Opens the Open File dialog box.
        /// </summary>
        /// <param name="fileMask"></param>
        /// <returns></returns>
        public string OpenFileOpenDialogBox(string extension, string filter)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentException("extension cannot be blank", "extension");
            }
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            if (string.IsNullOrWhiteSpace(filter))
            {
                throw new ArgumentException("filter cannot be blank", "filter");
            }

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = extension;
            dlg.Filter = filter;

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result.HasValue && result == true)
            {
                // Open document
                return dlg.FileName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Opens the Safe File dialog box.
        /// </summary>
        /// <returns></returns>
        public string OpenFileSaveDialogBox(string extension, string filter)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("v");
            }
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentException("extension cannot be blank", "extension");
            }
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            if (string.IsNullOrWhiteSpace(filter))
            {
                throw new ArgumentException("filter cannot be blank", "filter");
            }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = extension;
            dlg.Filter = filter;

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result.HasValue && result == true)
            {
                // Open document
                return dlg.FileName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Opens the given file (or URL) in the default application
        /// </summary>
        /// <param name="fileToOpen">The file path or URL to open</param>
        public void OpenInDefaultApplication(string fileToOpen)
        {
            if (fileToOpen == null)
            {
                throw new ArgumentNullException("fileToOpen");
            }
            if (string.IsNullOrWhiteSpace(fileToOpen))
            {
                throw new ArgumentException("fileToOpen cannot be spaces or an empty string.", "fileToOpen");
            }

            System.Diagnostics.Process.Start(fileToOpen);
        }
    }
}
