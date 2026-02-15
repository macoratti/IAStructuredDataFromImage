using Microsoft.Extensions.AI;

namespace IAStructuredDataFromImage.Application;

public static class PromptBuilder
{
    public static ChatMessage BuildSystemPrompt()
    {
        return new ChatMessage(ChatRole.System,
        """

        Extract all structured information from this receipt.

        Respond ONLY in valid JSON with this structure:

        {
          "storeName": "",
          "address": "",
          "date": "",
          "manager": "",
          "items": [
            {
              "name": "item name",
              "price": 0.00
            }
          ],
          "subtotal": 0.00,
          "tax": 0.00,
          "total": 0.00
        }

        Rules:
        - Do NOT invent quantities.
        - Do NOT create unitPrice.
        - Extract exactly what appears on the receipt.
        - Read every digit exactly as printed.
        - Do NOT round values.
        - Use period as decimal separator.
        - Ensure subtotal equals sum of item prices.
        - Ensure subtotal + tax equals total.
        - Output only valid JSON.

        """);
    }
}