var app = WebApplication.Create(args);

// GET /ping возвращает JSON и статус 200 OK.
app.MapGet("/ping", () => Results.Ok(new { message = "pong" }));

// Kestrel принимает запросы с этого компьютера.
app.Run("http://127.0.0.1:8001");
