using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Utility_Bot.Services;

namespace Utility_Bot.Controllers
{
    public class TextMessageController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            if (message.Text == "/start")
            {
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                        InlineKeyboardButton.WithCallbackData($" Количество символов" , $"1"),
                        InlineKeyboardButton.WithCallbackData($" Сумма чисел" , $"2")
                    });

                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Бот выполняет 2 функции:</b> {Environment.NewLine}" +
                $"<b>  1. Вычисление количества символов в отправленном сообщении.</b> {Environment.NewLine}" +
                $"<b>  2. Вычисление суммы отправленных чисел</b> {Environment.NewLine}" +
                $"{Environment.NewLine} Выберите функцию, нажав кнопку.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
            }
            else
            {
                string userFunctionCode = _memoryStorage.GetSession(message.Chat.Id).FunctionCode;
                switch (userFunctionCode)
                {
                    case "1":
                        await _telegramClient.SendTextMessageAsync(message.From.Id, $"Длина сообщения: {message.Text.Length} знаков", cancellationToken: ct);
                        break;
                    case "2":
                        await _telegramClient.SendTextMessageAsync(message.From.Id, $"Сумма введенных чисел равна {DigitExtractor.Extract(message.Text)}", cancellationToken: ct);
                        break;
                    default:
                        await _telegramClient.SendTextMessageAsync(message.From.Id, $"Выберите операцию из списка предложенных. Чтобы увидеть список введите /start. ", cancellationToken: ct);
                        break;
                }
            }
        }
    }
}
