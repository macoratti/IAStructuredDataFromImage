using IAStructuredDataFromImage.Domain;

namespace IAStructuredDataFromImage.Application;

public static class ReceiptValidator
{
    public static bool Validate(Receipt receipt, out List<string> errors)
    {
        errors = new List<string>();

        if (receipt == null)
        {
            errors.Add("Receipt is null.");
            return false;
        }

        // 1️⃣ Campos obrigatórios
        if (string.IsNullOrWhiteSpace(receipt.StoreName))
            errors.Add("Store name is missing.");

        if (string.IsNullOrWhiteSpace(receipt.Address))
            errors.Add("Address is missing.");

        if (string.IsNullOrWhiteSpace(receipt.Manager))
            errors.Add("Manager name is missing.");

        if (receipt.Date == default)
            errors.Add("Invalid or missing date.");

        // 2️⃣ Itens
        if (receipt.Items == null || receipt.Items.Count == 0)
        {
            errors.Add("No line items found.");
        }
        else
        {
            foreach (var item in receipt.Items)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    errors.Add("Item with missing name detected.");

                if (item.Price < 0)
                    errors.Add($"Negative price detected for item: {item.Name}");
            }
        }

        // 3️ Soma dos itens = Subtotal
        var calculatedSubtotal = receipt.Items?.Sum(i => i.Price) ?? 0;

        if (Math.Abs(calculatedSubtotal - receipt.Subtotal) > 0.01m)
        {
            errors.Add(
                $"Subtotal mismatch. " +
                $"Calculated: {calculatedSubtotal:F2}, " +
                $"Reported: {receipt.Subtotal:F2}");
        }

        // 4️ Subtotal + Tax = Total
        var calculatedTotal = receipt.Subtotal + receipt.Tax;

        if (Math.Abs(calculatedTotal - receipt.Total) > 0.01m)
        {
            errors.Add(
                $"Total mismatch. " +
                $"Expected: {calculatedTotal:F2}, " +
                $"Reported: {receipt.Total:F2}");
        }

        // 5️ Valores negativos gerais
        if (receipt.Subtotal < 0)
            errors.Add("Subtotal cannot be negative.");

        if (receipt.Tax < 0)
            errors.Add("Tax cannot be negative.");

        if (receipt.Total < 0)
            errors.Add("Total cannot be negative.");

        return errors.Count == 0;
    }
}