using IAStructuredDataFromImage.Application;
using IAStructuredDataFromImage.Infrastructure;
using IAStructuredDataFromImage.Shared;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

CultureNormalization.ApplyInvariantCulture();

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOllamaVision(
    "http://localhost:11434",
    "llama3.2-vision:latest");

builder.Services.AddSingleton<ReceiptExtractionService>();

var app = builder.Build();

Console.WriteLine("Recebendo e analisando imagem...");
Console.WriteLine("Aguarde, isso pode levar alguns segundos...\n");

var extractor = app.Services.GetRequiredService<ReceiptExtractionService>();

var receipt = await extractor.ExtractAsync("receipts/receipt3.png");

if (receipt == null)
{
    Console.WriteLine("Falha ao extrair dados.");
    return;
}

Console.WriteLine($"Itens extraídos: {receipt.Items.Count}");

Console.WriteLine("\n=== DADOS EXTRAÍDOS ===\n");

Console.WriteLine($"Store: {receipt.StoreName}");
Console.WriteLine($"Address: {receipt.Address}");
Console.WriteLine($"Date: {receipt.Date}");
Console.WriteLine($"Manager: {receipt.Manager}");
Console.WriteLine($"Subtotal: {receipt.Subtotal}");
Console.WriteLine($"Tax: {receipt.Tax}");
Console.WriteLine($"Total: {receipt.Total}");

if (ReceiptValidator.Validate(receipt, out var errors))
{
    Console.WriteLine("\nRecibo válido.");
}
else
{
    Console.WriteLine("Erros encontrados:");
    foreach (var error in errors)
        Console.WriteLine($"- {error}");
}


Console.WriteLine("\n=== JSON GERADO ===\n");

var json = JsonSerializer.Serialize(
    receipt,
    new JsonSerializerOptions
    {
        WriteIndented = true
    });

Console.WriteLine(json);

Console.ReadLine();