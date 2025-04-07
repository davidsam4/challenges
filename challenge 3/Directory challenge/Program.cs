using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string directoryPath = "FileCollection";
        string resultFilePath = Path.Combine(directoryPath, "results.txt");
        
        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // File extensions for Office files
        string[] officeExtensions = { ".xlsx", ".docx", ".pptx" };
        
        // Counters and size trackers
        int wordCount = 0, excelCount = 0, pptCount = 0, totalCount = 0;
        long wordSize = 0, excelSize = 0, pptSize = 0, totalSize = 0;

        // Get all files in the directory
        DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
        FileInfo[] files = dirInfo.GetFiles();
        
        foreach (FileInfo file in files)
        {
            if (officeExtensions.Contains(file.Extension, StringComparer.OrdinalIgnoreCase))
            {
                totalCount++;
                totalSize += file.Length;
                
                switch (file.Extension.ToLower())
                {
                    case ".docx":
                        wordCount++;
                        wordSize += file.Length;
                        break;
                    case ".xlsx":
                        excelCount++;
                        excelSize += file.Length;
                        break;
                    case ".pptx":
                        pptCount++;
                        pptSize += file.Length;
                        break;
                }
            }
        }

        // Write results to file
        using (StreamWriter writer = new StreamWriter(resultFilePath))
        {
            writer.WriteLine("Microsoft Office File Summary:");
            writer.WriteLine($"Word Documents (.docx): {wordCount} files, {wordSize / 1024} KB");
            writer.WriteLine($"Excel Files (.xlsx): {excelCount} files, {excelSize / 1024} KB");
            writer.WriteLine($"PowerPoint Files (.pptx): {pptCount} files, {pptSize / 1024} KB");
            writer.WriteLine($"Total: {totalCount} files, {totalSize / 1024} KB");
        }

        Console.WriteLine("Summary written to results.txt");
    }
}
