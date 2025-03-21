// See https://aka.ms/new-console-template for more information

using LD.EventFlow.Ref.Listeners;
using LD.EventFlow.Ref.Messages;

Console.WriteLine("Start Chat Application");
List<string> chatMessages = new List<string>()
{
    "Hello World!",
    "How are you?",
    "Goodbye!",
    "See you later!",
    "Have a nice day!",
    "Welcome to the chat!",
    "What are you doing?",
    "Let's have a conversation!",
    "I love programming!",
};
ChatApplication chatApplication = new ChatApplication();
LD.Framework.EventFlow.EventFlow.Register(chatApplication);
Task.Run(function: () =>
{
    while (true)
    { 
        LD.Framework.EventFlow.EventFlow.Broadcast(new TestUserChatMessage()
        {
            ChatMessage =chatMessages[Random.Shared.Next(chatMessages.Count)]
        });
        Task.Delay(Random.Shared.Next(1500, 2500)).Wait();
    }
}).Wait();

LD.Framework.EventFlow.EventFlow.Unregister(chatApplication);




 