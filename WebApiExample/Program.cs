
using System.Net.Http.Json;
using System.Text.Json;
using WebApiExample;

var posts = default(List<Post>);

using (var client = new HttpClient())
{
    //запрос GET
    var result = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
    result.EnsureSuccessStatusCode(); //проверка что код двухсотый
    
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
        
    posts = JsonSerializer.Deserialize<List<Post>>(await result.Content.ReadAsStringAsync(), options);
}

// foreach (var post in posts.GroupBy(p=>p.UserId).Select(g=>g.Take(1)).Take(10))
// {
//     Console.WriteLine(JsonSerializer.Serialize(post));
// }

using (var client = new HttpClient())
{
    //отправляемый объект
    var newComment = new Comment { Id = 1, Name = "Ann", Email = "ann.smith@mail.ru" };
    
    //отправить запрос
    var response = await client.PostAsJsonAsync("https://jsonplaceholder.typicode.com/comments", newComment);
    var comment = await response.Content.ReadFromJsonAsync<Comment>();
    Console.WriteLine($"Id: {comment?.Id} - Name: {comment?.Name} - Email: {comment?.Email}");
}