using JanaTakmicenje22.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace JanaTakmicenje22.Core.Data
{
   public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<UserChallenge> UserChallenges { get; set; }
        public DbSet<Postignuca> Postignuca { get; set; }
        public DbSet<UserPostignuca> UserPostignuca { get; set; }

        private readonly string _dbPath;

        public AppDbContext()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var appFolder = Path.Combine(folder, "JanaTakmicenje22");
            Directory.CreateDirectory(appFolder);
            _dbPath = Path.Combine(appFolder, "jana.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Challenge>().HasData(
                new Challenge { Id = 1, Title = "3", Description = "Zapiši prve tri lepe stvari o sebi koje ti padnu na pamet.", Order = 1, XPReward = 80 },
                new Challenge { Id = 2, Title = "Prazno platno", Description = "Ostavi bilo šta što bi ti poremetilo pažnju sa strane na pola sata, uzmi olovku i papir koji ćeš staviti ispred sebe i samo sedi pola sata. Možda ti čak i padne na pamet da napišeš nešto :)).", Order = 2, XPReward = 90 },
                new Challenge { Id = 3, Title = "Priroda i čovek kao jedno", Description = "Hoću da izdvojiš danas sat vremena, ujutro bi bilo najbolje, i odeš na trčanje ili šetanje, ali bez telefona i slušalica.", Order = 3, XPReward = 100 },
                new Challenge { Id = 4, Title = "Sam sa sobom", Description = "Opet ćemo meditirati, ali ovaj put bez olovke i papira. Budi u tišini. Kad god osetišneku negativnu emociju, stani, vrati se u nazad i pokušaj da razmisliš zašto si je osetio.", Order = 4, XPReward = 100 },
                new Challenge { Id = 5, Title = "Komplimenti", Description = "Priđi nekoj osobi na ulici i udeli im kompliment. Malo teže nego prošli zadaci, ali uzmi u obzir da se niko nikad nije žalio na kompliment :).", Order = 5, XPReward = 150 },
                new Challenge { Id = 6, Title = "Neočekivani poziv", Description = "Umesto da pošalješ poruku, pozovi nekoga da ćaskate. Znam da najradije ne bi ni na poruku odgovorio ali testiraj se malo.", Order = 6, XPReward = 150 },
                new Challenge { Id = 7, Title = "Ne!", Description = "Kreni da vežbaš iskreno odgovarati ljudima. Traže od tebe da uradiš nešto što radije ne bi? Samo kaži ne! Naravno, biće ti neprijatno u početku, ali će mnogo gore posledice izazvati to što radiš stvari koje zapravo ne želiš.", Order = 7, XPReward = 160 },
                new Challenge { Id = 8, Title = "Mala pomoć", Description = "Traži od nekog radnika u prodavnici pomoć za nešto. Bolje nego da lutaš dva sata naokolo u nadi da ćeš naći šta tražiš samo zato što ne želiš da priđeš radniku.", Order = 8, XPReward = 130 },
                new Challenge { Id = 9, Title = "Dejt za jednog", Description = "Izađi negde sam. Kafić, bioskop ili restoran? Nije bitno. Samo izvedi sebe negde bez osećaja anksioznosti zato što te ljudi vide samog.", Order = 9, XPReward = 180 },
                new Challenge { Id = 10, Title = "Izlazak", Description = "Pozovi nekog na kafu. Iznenadi i ti malo ljude!", Order = 10, XPReward = 170 },
                new Challenge { Id = 11, Title = "Prvi put", Description = "Obrati se uslužnom radniku bez uvežbavanja teksta.", Order = 11, XPReward = 140 },
                new Challenge { Id = 12, Title = "Poruka podrške", Description = "Pošalji poruku nekome koga voliš, samo da im kažeš da misliš na njih.", Order = 12, XPReward = 120 },
                new Challenge { Id = 13, Title = "Izlazak 2", Description = "Malo ćemo da otežamo izlazak. Ovog puta želim da ideš na neku žurku, klub ili dešavanje gde se okuplja više ljudi.", Order = 13, XPReward = 200 },
                new Challenge { Id = 14, Title = "Bez osećaja sramote", Description = "Jedi na javnom mestu ispred drugih bez osećaja da te neko osuđuje.", Order = 14, XPReward = 150 },
                new Challenge { Id = 15, Title = "Broj", Description = "Pitaj nekoga koga ne poznaješ, a privlačan ti je, za njihov broj telefona.", Order = 15, XPReward = 250 },
                new Challenge { Id = 16, Title = "Small talk", Description = "Dok čekaš u redu za nešto probaj da pokreneš razgovor sa osobom iza sebe.", Order = 16, XPReward = 160 },
                new Challenge { Id = 17, Title = "Bez distrakcija!", Description = "Jedi obrok bez gledanja u ekran ili čitanja bilo čega.", Order = 17, XPReward = 110 },
                new Challenge { Id = 18, Title = "Neutralno lice", Description = "Kada ti se neko obraća nemoj da se smeškaš na nešto sa čim se ne slažeš. Neutralno lice.", Order = 18, XPReward = 140 },
                new Challenge { Id = 19, Title = "Umetnost Disanja", Description = "Fokusiraj se na udisanje i izdisanje 3 minuta dok sediš u parku.", Order = 19, XPReward = 100 },
                new Challenge { Id = 20, Title = "Već smo na pola puta!", Description = "Osvojio si prvu polovinu! Na pola si puta. Zapiši šta si naučio o sebi tokom ovih izazova.", Order = 20, XPReward = 300 },
                new Challenge { Id = 21, Title = "Samo pozitiva", Description = "Podeli sa nekim svoj uspeh na koji si ponosan.", Order = 21, XPReward = 150 },
                new Challenge { Id = 22, Title = "Izlazak 3", Description = "Opet izlazak...E pa ovog puta ćeš izaći BEZ ikoga, ideš na socijalno okupljanje sam. Verujem u tebe.", Order = 22, XPReward = 220 },
                new Challenge { Id = 23, Title = "Šetnja sa osmehom", Description = "Dok se budeš šetao, želim da se osmehneš petorici ljudi koje budeš sreo usput.", Order = 23, XPReward = 160 },
                new Challenge { Id = 24, Title = "Zelena Terapija", Description = "Provedi pola sata u prirodi ili pored biljaka.", Order = 24, XPReward = 120 },
                new Challenge { Id = 25, Title = "Mala pomoć 2", Description = "Ovog puta ti budi taj koji će da priđe osobi kojoj treba pomoć.", Order = 25, XPReward = 180 },
                new Challenge { Id = 26, Title = "Nema guglanja", Description = "Ako osetiš simptom stresa, nemoj guglati dijagnoze sat vremena.", Order = 26, XPReward = 130 },
                new Challenge { Id = 27, Title = "Glasni smeh", Description = "Pogledaj video koji te uvek nasmeje do suza.", Order = 27, XPReward = 100 },
                new Challenge { Id = 28, Title = "Držanje tela", Description = "Ispravi se i drži glavu visoko dok hodaš ulicom.", Order = 28, XPReward = 130 },
                new Challenge { Id = 29, Title = "Bez izvinjenja", Description = "Pokušaj da provedeš dan bez nepotrebnog izvinjenja za sitnice.", Order = 29, XPReward = 150 },
                new Challenge { Id = 30, Title = "Zdrav život", Description = "Pridruži se nekom klubu za trčanje. Izlaziš iz zone komfora u oba slučaja.", Order = 30, XPReward = 200 },
                new Challenge { Id = 31, Title = "Neuobičajeno", Description = "Kad budeš šetao ponesi sa sobom šolju od kuće ili tanjir iz kojeg ćeš jesti/piti.", Order = 31, XPReward = 170 },
                new Challenge { Id = 32, Title = "Obrnuto", Description = "Hoću šta god negativno da se desilo danas iz njega izvučeš nešto pozitivno, kako god umeo.", Order = 32, XPReward = 160 },
                new Challenge { Id = 33, Title = "Mali poklon", Description = "Kupi sebi neku sitnicu (cvet, olovku) kao nagradu za trud.", Order = 33, XPReward = 120 },
                new Challenge { Id = 34, Title = "3 osobe", Description = "Kada budeš izašao predstavi se bar troje ljudi.", Order = 34, XPReward = 210 },
                new Challenge { Id = 35, Title = "Vizuelizacija", Description = "Zamisl isebe u situaciji koja te plaši, ali kako si potpuno smiren.", Order = 35, XPReward = 150 },
                new Challenge { Id = 36, Title = "Razgovor sa sobom", Description = "Obraćaj se sebi danas kao što bi se obraćao/la najboljom prijatelju/ici.", Order = 36, XPReward = 140 },
                new Challenge { Id = 37, Title = "Društvena mreža", Description = "Otključaj svoj profil. Budi otvoren za bilo kakvo osuđivanje.", Order = 37, XPReward = 200 },
                new Challenge { Id = 38, Title = "Pomozi drugome", Description = "Uradi jedno malo dobro delo za nekoga, anonimno.", Order = 38, XPReward = 180 },
                new Challenge { Id = 39, Title = "Samoodbrana", Description = "Ako neko govori ili radi nešto protiv tebe, ne ćuti nego se odbrani!", Order = 39, XPReward = 220 },
                new Challenge { Id = 40, Title = "Bravooo!!!", Description = "Završio/la si sve predviđene zadatke. Nadam se da si primetio/la neki napredak u odnosu na početak. Budi ponosan/ponosna na sebe!", Order = 40, XPReward = 500 }
            );

            modelBuilder.Entity<Postignuca>().HasData(
                new Postignuca { Id = 1, Name = "Početak putovanja", Description = "Završio/la si prvi izazov -`♡´-", Emoji = "˚₊‧꒰ა 𓂋 ໒꒱ ‧₊˚", RequiredChallenges = 1 },
                new Postignuca { Id = 2, Name = "Dobar početak", Description = "5 izazova završeno -`♡´-", Emoji = "₍ᐢ. .ᐢ₎ ₊˚⊹♡", RequiredChallenges = 5 },
                new Postignuca { Id = 3, Name = "Osvojio si polovinu", Description = "20 izazova završeno -`♡´-", Emoji = "ᓚ₍⑅^..^₎♡", RequiredChallenges = 20 },
                new Postignuca { Id = 4, Name = "Blizu si..", Description = "30 izazova završeno -`♡´-", Emoji = "── ⋆⋅☆⋅⋆ ──", RequiredChallenges = 30 },
                new Postignuca { Id = 5, Name = "Šampion", Description = "Svi izazovi završeni -`♡´-", Emoji = "( ˶˘ ³˘)♡", RequiredChallenges = 40 },
                new Postignuca { Id = 6, Name = "Deset dana! Idemooo", Description = "10 izazova završeno -`♡´-", Emoji = "✧｡٩(ˊᗜˋ )و✧*｡", RequiredChallenges = 10 }
            );
        }

    }
}
