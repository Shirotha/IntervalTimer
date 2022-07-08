namespace Shirotha.Options
{
    // TODO: add namespaces with push/pop blocks
    public class Options
    {
        List<string> _Unnamed = new();
        Dictionary<string, string> _Options = new();

        public Options(IEnumerable<string> args)
        {
            ReadOptions(args);
        }

        void ReadOptions(IEnumerable<string> args)
        {
            string? name = null;
            foreach (var arg in args)
            {
                if (name is null)
                {
                    if (arg.StartsWith("-"))
                        name = arg;
                    else
                        _Unnamed.Add(arg);
                }
                else
                {
                    _Options.TryAdd(name, arg);
                    name = null;
                }
            }
            if (name is not null)
                throw new ArgumentException("Last option is missing a value.", nameof(args));
        }

        public object Get(string name, Type type, object @default) => type.Name switch
        {
            nameof(String) => _Options.TryGetValue(name, out var value) ? value : @default,
            nameof(Int32) => _Options.TryGetValue(name, out var value) ? (int.TryParse(value, out var result) ? result : throw new ArgumentException("Can't convert option to integer", nameof(name))) : @default,
            // TODO: add more types
            _ => throw new ArgumentException("Unsupported value for type.", nameof(type))
        };

        public T Get<T>(string name, T @default) 
            where T : notnull 
            =>
            (T)Get(name, typeof(T), @default);

        public IReadOnlyList<string> Unnamed => _Unnamed;
    }
}