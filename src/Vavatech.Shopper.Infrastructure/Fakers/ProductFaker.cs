using Bogus;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure.Fakers;

// dotnet add package Bogus
public class ProductFaker : Faker<Product>
{
	public ProductFaker()
	{

        Dictionary<int, Tag> tags = new()
        {
            {1, new Tag {Id = 1, Title = "Software" } },
            {2, new Tag {Id = 2, Title = "Clothing" } },
            {3, new Tag {Id = 3, Title = "Hardware" } },
        };

        StrictMode(true);
        RuleFor(p => p.Id, f => f.IndexFaker + 1);
		RuleFor(p => p.Name, f => f.Commerce.ProductName());
        RuleFor(p => p.Description, f => f.Commerce.ProductDescription());
		RuleFor(p => p.Color, f => f.Commerce.Color());
		RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()));
        RuleFor(p => p.Tags, f => f.PickRandom(tags.Values, f.Random.Int(1,tags.Count)));

        RuleFor(p => p.Size, f => f.PickRandom<Size>().OrNull(f, 0.5f));
	}
}
