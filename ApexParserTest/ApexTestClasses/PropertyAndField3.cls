public class PropertyAndField
{
    transient Integer TransientInteger;
    public DateTime DateTimeGetSet { get; set; }
    public List<DateTime> DateTimeGetSetGeneric { get; set; }
    public DateTime[] DateTimeGetSetArray { get; set; }
    public DateTime DateTimeEmpty;
    public DateTime DateTimeInitialized = DateTime.now();
    public List<DateTime> DateTimeList = new List<DateTime>();
    public DateTime[] DateTimeArray = new DateTime[5];
    public String Name = 'jay';
    public final String nameFinal = 'jay';
    public static String NameStatic = 'jay';
    public static final String NameStaticFinal = 'jay';

    public Set<String> stringSet = new Set<String>{};

    public boolean shouldRedirect { get;set; }
    {
        shouldRedirect =false;
    }

    public String[] newStringArray = new String[] {'Hi'};

    public void MethodOne()
    {
        DateTime dateTimeEmpty;
        dateTimeEmpty = DateTime.now();
        DateTime dateTimeInitilized = DateTime.now();
        List<DateTime> dateTimeList = new List<DateTime>();
        DateTime[] dateTimeArrary = new DateTime[5];
        String name;
        name = 'Jay';
    }
}
