# Configuration.Substitution
Automatically substitute placeholders in your configuration from added sources.

## Usage
You can view a full example in the Configuration.Substitution.Tester project.
### settings.json
```json
{
  "Endpoint": "https://www.awesomeSauce.${env}.azure.com"
}
```
### Program.cs
```csharp
static async Task Main(string[] args){
    await Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder => {
            builder.AddJsonFile("settings.json");
            builder.AddInMemoryCollection(new[]{
                new KeyValuePair<string, string?>("env", "prod") // This can be environmental variables or any other source
            });
            builder.EnableSubstitution(); // This is where the magic happens!
        })
        .ConfigureServices((context, collection) => {
            collection.Configure<Settings>(context.Configuration);
            collection.AddHostedService<BGService>();
        })
        .RunConsoleAsync(options => {
            options.SuppressStatusMessages = true;
        });
    }
```
### Output
```shell
info: Configuration.Substitution.Tester.BGService[0]
      BGService resolved endpoint https://www.awesomeSauce.prod.azure.com
```

## License

MIT License

Copyright (c) 2025 Greg James

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
