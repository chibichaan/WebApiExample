
using System.Net.Http.Json;
using System.Text.Json;
using WebApiExample;

var posts = default(List<Post>);

//запрос GET
using (var client = new HttpClient())
{
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

//запрос POST
using (var client = new HttpClient())
{
    //отправляемый объект
    var newComment = new Comment { Id = 1, Name = "Ann", Email = "ann.smith@mail.ru" };
    
    //отправить запрос
    var response = await client.PostAsJsonAsync("https://jsonplaceholder.typicode.com/comments", newComment);
    var comment = await response.Content.ReadFromJsonAsync<Comment>();
    
    //Console.WriteLine($"Id: {comment?.Id} - Name: {comment?.Name} - Email: {comment?.Email}");
}

//запрос PUT
using (var client = new HttpClient())
{
    //обновить полностью информацию
    var response = await client.PutAsJsonAsync("https://jsonplaceholder.typicode.com/albums", 
        new Album(){Id = 1, UserId = 1, Title = "update album"});

    //response.EnsureSuccessStatusCode();

    var album = await response.Content.ReadFromJsonAsync<Album>();
    //Console.WriteLine($"{album}\n");
}

//запрос DELETE

using (var client = new HttpClient())
{
    var response = await client.DeleteAsync("https://jsonplaceholder.typicode.com/todos");
    //response.EnsureSuccessStatusCode();

    var jsonResponse = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"{jsonResponse}\n");
}


//запрос PATCH не делала, потому что put-запрос (не) работает плохо