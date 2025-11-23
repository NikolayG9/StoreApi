using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Store.Application.Services.Interfaces;
using Store.Domain.Repositories;

namespace Store.Application.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        private readonly IOrderRepository _orderRepository;

        public PdfGeneratorService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<byte[]> GenerateOrderPdfFileAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId, cancellationToken);

            var pdfBytes = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);

                    // -------------------------
                    // HEADER
                    // -------------------------
                    page.Header().PaddingBottom(20).Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Elegant Bride Boutique")
                                .FontSize(20).Bold();

                            col.Item().Text($"Order Date: {order.OrderDate:yyyy-MM-dd HH:mm}");
                            col.Item().Text($"Order ID: {order.Id}");
                        });
                    });

                    // -------------------------
                    // CONTENT
                    // -------------------------
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        // Customer Info Section
                        col.Item().Border(1).Padding(10).Column(cust =>
                        {
                            cust.Item().Text("Customer Information")
                                .FontSize(14).SemiBold();

                            cust.Spacing(5);

                            cust.Item().Text($"{order.OrderInformation.FirstName} {order.OrderInformation.LastName}");
                            cust.Item().Text($"{order.OrderInformation.Address}, {order.OrderInformation.City}, {order.OrderInformation.PostalCode}, {order.OrderInformation.Country}");
                            cust.Item().Text($"Phone: {order.OrderInformation.PhoneNumber}");
                            cust.Item().Text($"Email: {order.OrderInformation.Email}");

                            if (!string.IsNullOrWhiteSpace(order.OrderInformation.OrderDetails))
                            {
                                cust.Spacing(5);
                                cust.Item().Text($"Notes: {order.OrderInformation.OrderDetails}");
                            }
                        });

                        col.Spacing(20);

                        // Products Table
                        col.Item().Element(container =>
                        {
                            container.Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);   // product + collection
                                    columns.RelativeColumn(1);   // color
                                    columns.RelativeColumn(1);   // size
                                    columns.RelativeColumn(1);   // qty
                                    columns.RelativeColumn(1);   // unit price
                                    columns.RelativeColumn(1);   // total
                                });

                                // HEADER
                                table.Header(header =>
                                {
                                    header.Cell().Element(Th).Text("Product");
                                    header.Cell().Element(Th).Text("Color");
                                    header.Cell().Element(Th).Text("Size");
                                    header.Cell().Element(Th).Text("Qty");
                                    header.Cell().Element(Th).Text("Unit Price");
                                    header.Cell().Element(Th).Text("Total");
                                });

                                // ROWS
                                foreach (var p in order.OrderedProducts)
                                {
                                    decimal unitPrice = p.Price;
                                    decimal discount = p.Discount ?? 0;
                                    decimal effectivePrice = unitPrice - discount;
                                    decimal lineTotal = effectivePrice * p.ProductQuantity;

                                    table.Cell().Element(Td)
                                        .Text($"{p.Name} ({p.CollectionName})");

                                    table.Cell().Element(Td).Text(p.SelectedColor);
                                    table.Cell().Element(Td).Text(p.SelectedSize);
                                    table.Cell().Element(Td).Text(p.ProductQuantity.ToString());
                                    table.Cell().Element(Td).Text($"{effectivePrice:C}");
                                    table.Cell().Element(Td).Text($"{lineTotal:C}");
                                }
                            });

                            // Styled cells
                            static IContainer Td(IContainer c) =>
                                c.BorderBottom(0.5f).PaddingVertical(5).PaddingHorizontal(2);

                            static IContainer Th(IContainer c) =>
                                c.Background(Colors.Grey.Lighten3).Padding(5);
                        });

                        col.Spacing(20);

                        // Totals Section
                        col.Item().AlignRight().Column(sum =>
                        {
                            decimal subtotal = order.OrderedProducts.Sum(p => p.Price * p.ProductQuantity);
                            decimal totalDiscount = order.OrderedProducts.Sum(p => (p.Discount ?? 0) * p.ProductQuantity);
                            decimal finalTotal = subtotal - totalDiscount;

                            sum.Item().Text($"Subtotal: {subtotal:C}");

                            if (totalDiscount > 0)
                            {
                                sum.Item().Text($"Discount: -{totalDiscount:C}")
                                    .FontColor(Colors.Red.Medium);
                            }

                            sum.Spacing(7);
                            sum.Item().Text($"TOTAL: {finalTotal:C}")
                                .FontSize(14).Bold();
                        });
                    });

                    // Footer
                    page.Footer().AlignCenter()
                        .Text($"Generated on {DateTime.Now:yyyy-MM-dd HH:mm}");
                });
            }).GeneratePdf();

            return pdfBytes;
        }
    }
}
