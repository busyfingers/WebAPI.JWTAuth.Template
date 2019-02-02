using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Extensions.Logging;

namespace WebAPI.JWTAuth.Template.Helpers
{
    public static class FileUtils
    {
        public static void CompressLogs(ILogger logger)
        {
            var logsFolder = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            var archiveFolder = Path.Combine(logsFolder, "archive");

            logger.LogInformation($"Compressing files in {logsFolder} to {archiveFolder}");

            if (EnsureDirectory(archiveFolder))
            {
                try
                {
                    Compress(logger, logsFolder, archiveFolder);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Could not compress logs.");
                }
            }
        }

        /// <summary>
        /// Compress log files in the source directory and puts the compressed files in the target directory.
        /// Skips today's log file.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="sourceDir"></param>
        /// <param name="targetDir"></param>
        private static void Compress(ILogger logger, string sourceDir, string targetDir)
        {
            var directoryToCompress = new DirectoryInfo(sourceDir);

            foreach (var fileToCompress in directoryToCompress.GetFiles())
            {
                if ((File.GetAttributes(fileToCompress.FullName)
                    & FileAttributes.Hidden) != FileAttributes.Hidden
                    & fileToCompress.Extension != ".gz"
                    & !fileToCompress.FullName.Contains(DateTime.Now.ToString("yyyyMMdd")))
                {
                    using (var originalFileStream = fileToCompress.OpenRead())
                    {
                        using (var compressedFileStream = File.Create(Path.Combine(targetDir, fileToCompress.Name + ".gz")))
                        {
                            using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        }

                        var info = new FileInfo(Path.Combine(targetDir, fileToCompress.Name + ".gz"));
                        logger.LogInformation($"{fileToCompress.Name}: {fileToCompress.Length.ToString()} -> {info.Length.ToString()} bytes.");
                    }

                    fileToCompress.Delete();
                }
            }
        }

        public static bool EnsureDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
