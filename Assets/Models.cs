using System.Collections.Generic;

[System.Serializable]
public class Pokemon {

    public int id;
    public string name;
    public Sprites sprites;
    public List<Type> types;
}

[System.Serializable]
public class Sprites {
    public string front_default;
}

[System.Serializable]
public class Type {
    public int slot;
    public Type2 type;
}

[System.Serializable]
public class Type2 {
    public string name;
}

[System.Serializable]

public class Species {
    public List<FlavorTextEntry> flavor_text_entries;
    public List<Genera> genera;
}

[System.Serializable]
public class FlavorTextEntry {
    public string flavor_text;
    public Language language;
}

[System.Serializable]
public class Genera {
    public string genus;
    public Language language;
}

[System.Serializable]
public class Language {
    public string name;
}
