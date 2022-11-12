namespace AI2.Entities {
    public class Item {
        public Item(string nazwa, float masa, float wartosc) {
            Nazwa = nazwa;
            Masa = masa;
            Wartosc = wartosc;
        }

        public string Nazwa { get; set; }
        public float Masa { get; set; }
        public float Wartosc { get; set; }
    }
}
