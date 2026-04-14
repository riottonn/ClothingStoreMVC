using ClosedXML.Excel;
using ClothingStore.Domain;
using ClothingStore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Xceed.Document.NET;
using Xceed.Words.NET;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Infrastructure;
using ClothingStore.Domain;
using ClosedXML.Excel;
using Xceed.Words.NET; // Тепер це буде з пакету DocX
using System.IO;

namespace ClothingStore.Infrastructure.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ClothingStoreContext _context;

        public ReportsController(ClothingStoreContext context)
        {
            _context = context;
        }

        // EXCEL ПРАЦЮЄ БЕЗ ЗМІН
        public async Task<IActionResult> ExportToExcel()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Товари");
                worksheet.Cell(1, 1).Value = "Назва";
                worksheet.Cell(1, 2).Value = "Категорія";
                worksheet.Cell(1, 3).Value = "Ціна";
                worksheet.Row(1).Style.Font.Bold = true;

                int row = 2;
                foreach (var p in products)
                {
                    worksheet.Cell(row, 1).Value = p.Name;
                    worksheet.Cell(row, 2).Value = p.Category?.CategoryName ?? "Немає";
                    worksheet.Cell(row, 3).Value = p.Price;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductsReport.xlsx");
                }
            }
        }

        // WORD ТЕПЕР БЕЗ ПОМИЛКИ ПРО ЛІЦЕНЗІЮ
        public async Task<IActionResult> ExportToWord()
        {
            var products = await _context.Products.ToListAsync();
            using (var stream = new MemoryStream())
            {
                // Використовуємо DocX.Create
                using (var doc = DocX.Create(stream))
                {
                    doc.InsertParagraph("Звіт по товарах магазину").FontSize(18).Bold().Alignment = Alignment.center;

                    // Створюємо таблицю
                    var table = doc.AddTable(products.Count + 1, 2);
                    table.Design = TableDesign.TableGrid;
                    table.Rows[0].Cells[0].Paragraphs[0].Append("Товар");
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Ціна");

                    for (int i = 0; i < products.Count; i++)
                    {
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append(products[i].Name ?? "Без назви");
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(products[i].Price.ToString());
                    }

                    doc.InsertTable(table);
                    doc.Save();
                }
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ProductsReport.docx");
            }
        }
    }
}