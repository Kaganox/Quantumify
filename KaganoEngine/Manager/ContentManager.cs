using Raylib_cs;
namespace KaganoEngine.Manager;

public class ContentManager : IDisposable
{

    private readonly List<object> _content;
    private readonly Dictionary<Type, IProcessor> _processor;

    public ContentManager()
    {
        _content = new List<object>();
        _processor = new Dictionary<Type, IProcessor>();
        _processor.Add(typeof(Texture2D), new TextureProcessor());
        _processor.Add(typeof(Model), new ModelProcessor());
    }

    public T Load<T>(string path)
    {
        if(_processor.TryGetValue(typeof(T), out IProcessor? processor))
        {
            return (T)processor.Load<T>($"content/{path}");
        }
        return default!;
    }

    public void Unload(object texture)
    {
        Type type = texture.GetType();
        TryGetProcessor(type, (a) => {
            _processor[type].Unload(texture);
            _content.Remove(texture);
        });
    }

    public IProcessor TryGetProcessor(Type type,Action<IProcessor> action)
    {
        if (_processor.TryGetValue(type, out IProcessor? processor))
        {
            action.Invoke(processor);
            return processor;
        }
        Console.WriteLine($"No processor found for {type}");
        return null!;
    }

    public void Dispose()
    {
        _content.ForEach(content => {
            IProcessor processor = TryGetProcessor(content.GetType(), (a) => {
                a.Unload(content);
            });
         });
        //textures.Values.ToList().ForEach(texture => Raylib.UnloadTexture(texture));
    }
}
