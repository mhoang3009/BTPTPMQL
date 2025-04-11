using System;
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace BTPTPMQL.Models.Process
{
    public class ExcelProcess : Person 
    {
        public DataTable ExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Chỉ định license để tránh lỗi

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // Thêm cột từ dòng đầu tiên
                for (int col = 1; col <= colCount; col++)
                {
                    dt.Columns.Add(worksheet.Cells[1, col].Value?.ToString() ?? $"Column{col}");
                }

                // Thêm dữ liệu từ các dòng tiếp theo
                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        dr[col - 1] = worksheet.Cells[row, col].Value?.ToString();
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
    }
}
