namespace WebCompiler
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public static class FilePathHelper
    {
        //Code Reference - https://stackoverflow.com/a/275749
        public static string GetRelativePath(string fromDirectory, string toPath)
        {
            if (fromDirectory == null)
                throw new ArgumentNullException("fromDirectory");

            if (toPath == null)
                throw new ArgumentNullException("toPath");

            var isRooted = (Path.IsPathRooted(fromDirectory) && Path.IsPathRooted(toPath));

            if (isRooted)
            {
                var isDifferentRoot = string.Compare(Path.GetPathRoot(fromDirectory), Path.GetPathRoot(toPath), StringComparison.InvariantCultureIgnoreCase) != 0;

                if (isDifferentRoot)
                    return toPath;
            }

            var relativePath = new List<string>();
            var fromDirectories = fromDirectory.Split(Path.DirectorySeparatorChar);

            var toDirectories = toPath.Split(Path.DirectorySeparatorChar);

            var length = Math.Min(fromDirectories.Length, toDirectories.Length);

            var lastCommonRoot = -1;

            // find common root
            for (var x = 0; x < length; x++)
            {
                if (string.Compare(fromDirectories[x], toDirectories[x], StringComparison.InvariantCultureIgnoreCase) != 0)
                    break;

                lastCommonRoot = x;
            }

            if (lastCommonRoot == -1)
                return toPath;

            // add relative folders in from path
            for (var x = lastCommonRoot + 1; x < fromDirectories.Length; x++)
            {
                if (fromDirectories[x].Length > 0)
                    relativePath.Add("..");
            }

            // add to folders to path
            for (var x = lastCommonRoot + 1; x < toDirectories.Length; x++)
            {
                relativePath.Add(toDirectories[x]);
            }

            // create relative path
            var relativeParts = new string[relativePath.Count];
            relativePath.CopyTo(relativeParts, 0);

            var newPath = string.Join(Path.DirectorySeparatorChar.ToString(), relativeParts);

            return newPath;
        }
    }
}
