namespace Cosmos.Samples.Service.Web.Models;

public sealed record Order(
    string id,
    string customerName,
    DateTime submitted,
    bool fulfilled,
    decimal total
) : Item(
    id
);