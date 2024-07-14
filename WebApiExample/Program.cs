
using System.Text.Json;
using WebApiExample;

var posts = default(List<Post>);

using (var client = new HttpClient())
{
    //запрос
    var result = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
    result.EnsureSuccessStatusCode(); //проверка что код двухсотый

    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
        
    posts = JsonSerializer.Deserialize<List<Post>>(await result.Content.ReadAsStringAsync(), options);
}

foreach (var post in posts.GroupBy(p=>p.UserId).Select(g=>g.Take(1)).Take(10))
{
    Console.WriteLine(JsonSerializer.Serialize(post));
}