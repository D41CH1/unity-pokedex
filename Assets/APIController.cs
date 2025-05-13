using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class APIController : MonoBehaviour
{
    public RawImage pokeSprite;
    public TextMeshProUGUI pokeNameText, pokeNumText, pokeDescText, PokeSpeciesText;
    public TextMeshProUGUI[] pokeTypeTextArray;
    public GameObject[] pokeTypePanelArray;

    private readonly string baseURL = "https://pokeapi.co/api/v2/";

    private void Start() {

        pokeSprite.texture = Texture2D.blackTexture;
        pokeNameText.text = "Press button to start";
        pokeNumText.text = "####";
        PokeSpeciesText.text = "---";
        pokeDescText.text = "---";

        foreach (TextMeshProUGUI pokeTypeText in pokeTypeTextArray) {
            pokeTypeText.text = "";
        }

        foreach (GameObject pokeTypePanel in pokeTypePanelArray) {
            pokeTypePanel.GetComponent<UnityEngine.UI.Image>().color = new Color32(0, 0, 0, 0);
        }
    }

    public void RandonPokemon() {

        int randomIndex = UnityEngine.Random.Range(1, 1026);

        pokeSprite.texture = Texture2D.blackTexture;

        pokeNameText.text = "Loading...";
        pokeNumText.text = randomIndex.ToString("D" + 4);
        PokeSpeciesText.text = "...";
        pokeDescText.text = "...";

        foreach (TextMeshProUGUI pokeTypeText in pokeTypeTextArray) {
            pokeTypeText.text = "";
        }

        foreach (GameObject pokeTypePanel in pokeTypePanelArray) {
            pokeTypePanel.GetComponent<UnityEngine.UI.Image>().color = new Color32(0, 0, 0, 0);
        }

        GetPokemonAtIndex(randomIndex);
    }

    public async void GetPokemonAtIndex(int pokeIndex) {

        string pokeURL = baseURL + "pokemon/" + pokeIndex.ToString();
        string speciesURL = baseURL + "pokemon-species/" + pokeIndex.ToString();
        string lang = "en";

        var httpClient = new HttpClient(new JsonSerializationOption());

        var pokemon = await httpClient.Get<Pokemon>(pokeURL);
        var species = await httpClient.Get<Species>(speciesURL);
        var sprite = await httpClient.GetTexture(pokemon.sprites.front_default);

        string pokeName = pokemon.name;

        pokeSprite.texture = sprite;
        pokeSprite.texture.filterMode = FilterMode.Point;

        pokeNameText.text = CapitalizeFirstLetter(pokeName);

        for (int i = 0; i < pokemon.types.Count; i++) {
            pokeTypeTextArray[i].text = pokemon.types[i].type.name;
            pokeTypeTextArray[i].outlineColor = TypeOutlineColor(pokemon.types[i].type.name);

            pokeTypePanelArray[i].GetComponent<Image>().color = TypePanelColor(pokemon.types[i].type.name);
            pokeTypePanelArray[i].GetComponent<Outline>().effectColor = TypeOutlineColor(pokemon.types[i].type.name);
        }

        for (int i = 0; i < species.flavor_text_entries.Count; i++) {
            if (species.flavor_text_entries[i].language.name == lang) {
                pokeDescText.text = FixFormatting(species.flavor_text_entries[i].flavor_text);
                break;
            }
        }

        for (int i = 0; i < species.genera.Count; i++) {
            if (species.genera[i].language.name == lang) {
                PokeSpeciesText.text = species.genera[i].genus;
                break;
            }
        }
    }

    private string CapitalizeFirstLetter(string str) {
        return char.ToUpper(str[0]) + str.Substring(1);
    }

    private string FixFormatting(string str) {
        str = str.Replace("\n", " ");
        str = str.Replace("\f", " ");
        str = str.Replace("  ", " ");
        return str;
    }

    private Color32 TypePanelColor(string type) {
        Color32 typeClr;

        switch (type) {
            case "normal":
                typeClr = new Color32(168, 167, 122, 255);
                break;
            case "fire":
                typeClr = new Color32(238, 129, 48, 255);
                break;
            case "water":
                typeClr = new Color32(99, 144, 240, 255);
                break;
            case "grass":
                typeClr = new Color32(122, 199, 76, 255);
                break;
            case "electric":
                typeClr = new Color32(247, 208, 44, 255);
                break;
            case "ice":
                typeClr = new Color32(150, 217, 214, 255);
                break;
            case "fighting":
                typeClr = new Color32(194, 46, 40, 255);
                break;
            case "poison":
                typeClr = new Color32(163, 62, 161, 255);
                break;
            case "ground":
                typeClr = new Color32(226, 191, 101, 255);
                break;
            case "flying":
                typeClr = new Color32(169, 143, 243, 255);
                break;
            case "psychic":
                typeClr = new Color32(249, 85, 135, 255);
                break;
            case "bug":
                typeClr = new Color32(166, 185, 26, 255);
                break;
            case "rock":
                typeClr = new Color32(182, 161, 54, 255);
                break;
            case "ghost":
                typeClr = new Color32(115, 87, 151, 255);
                break;
            case "dragon":
                typeClr = new Color32(111, 53, 252, 255);
                break;
            case "steel":
                typeClr = new Color32(183, 183, 206, 255);
                break;
            case "dark":
                typeClr = new Color32(112, 87, 70, 255);
                break;
            case "fairy":
                typeClr = new Color32(214, 133, 173, 255);
                break;
            default:
                typeClr = new Color32(0, 0, 0, 0);
                break;
        }

        return typeClr;
    }

    private Color32 TypeOutlineColor(string type) {
        Color32 typeClr;

        switch (type) {
            case "normal":
                typeClr = new Color32(68, 67, 22, 255);
                break;
            case "fire":
                typeClr = new Color32(138, 29, 0, 255);
                break;
            case "water":
                typeClr = new Color32(0, 44, 140, 255);
                break;
            case "grass":
                typeClr = new Color32(22, 99, 0, 255);
                break;
            case "electric":
                typeClr = new Color32(147, 108, 0, 255);
                break;
            case "ice":
                typeClr = new Color32(50, 117, 114, 255);
                break;
            case "fighting":
                typeClr = new Color32(94, 0, 0, 255);
                break;
            case "poison":
                typeClr = new Color32(63, 0, 61, 255);
                break;
            case "ground":
                typeClr = new Color32(126, 91, 1, 255);
                break;
            case "flying":
                typeClr = new Color32(69, 43, 143, 255);
                break;
            case "psychic":
                typeClr = new Color32(149, 0, 35, 255);
                break;
            case "bug":
                typeClr = new Color32(66, 85, 0, 255);
                break;
            case "rock":
                typeClr = new Color32(82, 61, 0, 255);
                break;
            case "ghost":
                typeClr = new Color32(15, 0, 51, 255);
                break;
            case "dragon":
                typeClr = new Color32(11, 0, 152, 255);
                break;
            case "steel":
                typeClr = new Color32(83, 83, 106, 255);
                break;
            case "dark":
                typeClr = new Color32(12, 0, 0, 255);
                break;
            case "fairy":
                typeClr = new Color32(114, 33, 73, 255);
                break;
            default:
                typeClr = new Color32(0, 0, 0, 0);
                break;
        }

        return typeClr;
    }
    
}
