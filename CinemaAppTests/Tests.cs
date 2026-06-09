using Microsoft.VisualStudio.TestTools.UnitTesting;
using CinemaApp;

namespace CinemaApp.Tests
{
    [TestClass]
    public class ScreeningTests
    {
        private Screening CreateDefaultScreening() => new Screening("Inception", 5);

        // ---- Constructor ----

        [TestMethod]
        public void Constructor_ValidArguments()
        {
            {
                var screening = new Screening("Inception", 100);
                Assert.AreEqual("Inception", screening.GetTitle());
                Assert.AreEqual(100, screening.GetAvailableSeats());
            }
            {
                var screening = new Screening("Night of the Day of the Dawn of the Son of the Bride of the Return of the Revenge of the Terror of the Attack of the Evil, Mutant, Hellbound, Flesh-Eating Subhumanoid", 1);
                Assert.AreEqual("Night of the Day of the Dawn of the Son of the Bride of the Return of the Revenge of the Terror of the Attack of the Evil, Mutant, Hellbound, Flesh-Eating Subhumanoid", screening.GetTitle());
                Assert.AreEqual(1, screening.GetAvailableSeats());
            }
            {
                var screening = new Screening("RealMovie", 50);
                Assert.AreEqual("RealMovie", screening.GetTitle());
                Assert.AreEqual(50, screening.GetAvailableSeats());
            }
            {
                var screening = new Screening("ThisMovieNameDefinetlyExists", 24);
                Assert.AreEqual("ThisMovieNameDefinetlyExists", screening.GetTitle());
                Assert.AreEqual(24, screening.GetAvailableSeats());
            }
            {
                var screening = new Screening("/* ^.^ BCAS */", 9999);
                Assert.AreEqual("/* ^.^ BCAS */", screening.GetTitle());
                Assert.AreEqual(9999, screening.GetAvailableSeats());
            }
        }

        [TestMethod]
        public void Constructor_InvalidArguments()
        {
            Assert.ThrowsException<ArgumentException>(()=>new Screening("", 100));
            Assert.ThrowsException<ArgumentException>(()=>new Screening(" ", 100));
            Assert.ThrowsException<ArgumentException>(()=>new Screening("\n", 100));
            Assert.ThrowsException<ArgumentException>(()=>new Screening("\t", 100));
            Assert.ThrowsException<ArgumentException>(()=>new Screening("   ", 100));
            Assert.ThrowsException<ArgumentException>(()=>new Screening("Inception", 0));
            Assert.ThrowsException<ArgumentException>(()=>new Screening("Inception", -1));
            Assert.ThrowsException<ArgumentException>(()=>new Screening("Inception", -100));
            
        }
        // TODO: null vagy üres cím esetén ArgumentException-t kell dobni
        // TODO: totalSeats értéke 0 vagy negatív esetén ArgumentException-t kell dobni

        // ---- BookSeat ----

        [TestMethod]
        public void BookSeat_AvailableSeat()
        {
            var screening = CreateDefaultScreening();
            bool result = screening.BookSeat("Alice");
            Assert.IsTrue(result);
            Assert.AreEqual(4, screening.GetAvailableSeats());
            Assert.IsTrue(screening.BookSeat("RealPerson1"));
            Assert.AreEqual(3, screening.GetAvailableSeats());
            Assert.IsTrue(screening.BookSeat("RealPerson2"));
            Assert.AreEqual(2, screening.GetAvailableSeats());
            Assert.IsTrue(screening.BookSeat("RealPerson3"));
            Assert.AreEqual(1, screening.GetAvailableSeats());
            Assert.IsTrue(screening.BookSeat("RealPerson4"));
            Assert.AreEqual(0, screening.GetAvailableSeats());
        }

        [TestMethod]
        public void BookSeat_UnavailableSeat()
        {
            {
                var screening = CreateDefaultScreening();
                Assert.IsTrue(screening.BookSeat("RealPerson1"));
                Assert.AreEqual(4, screening.GetAvailableSeats());
                Assert.IsFalse(screening.BookSeat("RealPerson1"));
                Assert.IsFalse(screening.BookSeat("RealPerson1"));
                Assert.IsFalse(screening.BookSeat("RealPerson1"));
                Assert.AreEqual(4, screening.GetAvailableSeats());

                Assert.IsTrue(screening.BookSeat("RealPerson2"));
                Assert.IsTrue(screening.BookSeat("RealPerson3"));
                Assert.IsTrue(screening.BookSeat("RealPerson4"));
                Assert.IsFalse(screening.BookSeat("RealPerson5"));
                Assert.IsFalse(screening.BookSeat("RealPerson6"));



            }
            
        }
        // TODO: teli vetítésnél újabb foglalás false-t kell hogy visszaadjon
        // TODO: ugyanaz a személy kétszer próbál foglalni, másodszor false-t kell kapni
        // TODO: több különböző személy foglalása után a szabad helyek száma helyesen csökken

        // ---- CancelBooking ----

        [TestMethod]
        public void CancelBooking_ExistingBooking()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            Assert.AreEqual(4, screening.GetAvailableSeats());
            bool result = screening.CancelBooking("Alice");
            Assert.IsTrue(result);
            Assert.AreEqual(5, screening.GetAvailableSeats());

            screening.BookSeat("RealPerson1");
            bool result2 = screening.CancelBooking("RealPerson1");
            Assert.AreEqual(4, screening.GetAvailableSeats());
            Assert.IsTrue(result2);
            Assert.AreEqual(5, screening.GetAvailableSeats());
        }

        [TestMethod]
        public void CancelBooking_NotExistingBooking()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            Assert.AreEqual(4, screening.GetAvailableSeats());
            Assert.IsTrue(screening.CancelBooking("Alice"));
            Assert.IsFalse(screening.CancelBooking("Alice"));
            Assert.IsFalse(screening.CancelBooking("Alice"));
            Assert.IsFalse(screening.CancelBooking("Alice"));
            Assert.AreEqual(5, screening.GetAvailableSeats());

            Assert.IsFalse(screening.CancelBooking("FakePerson1"));
            Assert.AreEqual(5, screening.GetAvailableSeats());
        }
        // TODO: nem létező foglalás lemondásakor false-t kell visszaadni
        // TODO: lemondás után a személy neve már nem szerepel a foglaltak között

        // ---- IsBooked ----

        [TestMethod]
        public void IsBooked_AfterBooking()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            Assert.IsTrue(screening.IsBooked("Alice"));
            screening.BookSeat("RealPerson1");
            Assert.IsTrue(screening.IsBooked("RealPerson1"));
            Assert.IsTrue(screening.CancelBooking("RealPerson1"));
            Assert.IsFalse(screening.IsBooked("RealPerson1"));
        }

        [TestMethod]
        public void IsBooked_BeforeBooking()
        {
            var screening = CreateDefaultScreening();
            Assert.IsFalse(screening.IsBooked("FalsePerson1"));
            Assert.IsFalse(screening.IsBooked("FalsePerson1"));
            Assert.IsFalse(screening.IsBooked("FalsePerson2"));
        }
        // TODO: foglalás nélküli személyre false-t kell visszaadni
        // TODO: lemondás után ugyanarra a személyre false-t kell visszaadni

        // ---- GetAvailableSeats ----

        [TestMethod]
        public void GetAvailableSeats_AfterMultipleBookings()
        {
            var screening = CreateDefaultScreening(); // 5 férőhely
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.AreEqual(3, screening.GetAvailableSeats());
            Assert.IsTrue(screening.BookSeat("RealPerson2"));
            Assert.AreEqual(2, screening.GetAvailableSeats());
            Assert.IsTrue(screening.BookSeat("RealPerson3"));
            Assert.AreEqual(1, screening.GetAvailableSeats());
            Assert.IsTrue(screening.BookSeat("RealPerson4"));
            Assert.AreEqual(0, screening.GetAvailableSeats());
        }
        // TODO: újonnan létrehozott vetítésnél a szabad helyek száma egyenlő a totalSeats értékével
        // TODO: teli vetítésnél GetAvailableSeats() nullát kell visszaadni

        // ---- GetBookedCount ----

        [TestMethod]
        public void GetBookedCount_AfterBookings()
        {
            var screening = CreateDefaultScreening();
            Assert.AreEqual(0, screening.GetBookedCount());
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.AreEqual(2, screening.GetBookedCount());
            Assert.IsTrue(screening.CancelBooking("Alice"));
            Assert.AreEqual(1, screening.GetBookedCount());
            Assert.IsTrue(screening.CancelBooking("Bob"));
            Assert.AreEqual(0, screening.GetBookedCount());
        }
        // TODO: újonnan létrehozott vetítésnél GetBookedCount() nullát kell visszaadni
        // TODO: lemondás után a foglaltak száma helyesen csökken

        // ---- IsHouseFull ----

        [TestMethod]
        public void IsHouseFull_WhenAllSeatsBooked()
        {
            var screening = new Screening("Inception", 2);
            Assert.IsFalse(screening.IsHouseFull());
            screening.BookSeat("Alice");
            Assert.IsFalse(screening.IsHouseFull());
            screening.BookSeat("Bob");
            Assert.IsTrue(screening.IsHouseFull());
            screening.BookSeat("RandomCat");
            Assert.IsFalse(screening.IsHouseFull());
        }
        // TODO: szabad hellyel rendelkező vetítésnél false-t kell visszaadni
        // TODO: lemondás után a vetítés már nem teli, IsHouseFull() false-t ad vissza

        // -------------------------------------------------------
        // EXTRA FELADAT — Várólista tesztek
        // Az alábbi teszteket csak akkor vedd fel,
        // ha az alap feladattal már végzett vagy!
        // -------------------------------------------------------

        // [TestMethod]
        // public void AddToWaitingList_WhenFull()
        // {
        //     ...
        // }
        // TODO (extra): szabad hely esetén várólistára kerülés false-t kell hogy visszaadjon
        // TODO (extra): már foglalással rendelkező személy várólistára kerülése false-t kell hogy visszaadjon
        // TODO (extra): ugyanaz a személy kétszer próbál várólistára kerülni, másodszor false-t kap
        // TODO (extra): lemondás után az első várólistás személy automatikusan foglalást kap
        // TODO (extra): GetWaitingPosition nem létező személyre -1-et kell visszaadni
    }
}
