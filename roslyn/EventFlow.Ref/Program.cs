// See https://aka.ms/new-console-template for more information

using LD.EventSystem.Ref.Listeners;
using LD.EventSystem.Ref.Messages;

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
LD.EventSystem.EventFlow.Register(chatApplication);

// 100마리의 몬스터 있음
// 100개의 체력바있음
// 한 마리만 데미지입어도 100개의 체력바가 몬스터 공격당함 이벤트에 반응

Task.Run(function: () =>
{
    while (true)
    { 
        LD.EventSystem.EventFlow.Broadcast(new TestUserChatMessage()
        {
            ChatMessage =chatMessages[Random.Shared.Next(chatMessages.Count)]
        });
        Task.Delay(Random.Shared.Next(1500, 2500)).Wait();
    }
}).Wait();

LD.EventSystem.EventFlow.UnRegister(chatApplication);




 