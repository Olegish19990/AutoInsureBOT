# 🚗 AutoInsureBot

**AutoInsureBot** is a C# Telegram bot that assists users in purchasing car insurance. It guides users through uploading documents, confirms the extracted data, proposes a fixed price, and generates a final PDF insurance policy document.

---

## ✨ Features

- 🤖 Telegram bot interface for user interaction
- 📸 Document image input (passport & vehicle registration)
- 🧠 OCR data extraction via [Mindee API](https://www.mindee.com/)
- ✅ Confirmation dialogs for passport and vehicle data
- 💵 Fixed price quote (100 USD)
- 📄 PDF policy generation using [QuestPDF](https://www.questpdf.com/)
- 🔁 Session management with restart option

---


## Setup
To run the bot, you need to declare two configuration files with your API tokens:

appsettings.json: This file should contain your Mindee API token.

{
  "Mindee": {
    "Token": "your_mindee_api_token"
  }
}

secret.json: This file should contain your Telegram Bot Token.

{
  "TelegramBotToken": "your_telegram_bot_token"
}
