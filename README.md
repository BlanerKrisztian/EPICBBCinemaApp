# Vizsga: Mozi-vetítés — Unit tesztelés & Git

## Áttekintés

Egy részben elkészített C# solution-t kapsz.  
Az osztály és a metódus-szignatúrák adottak, de **a logika nincs implementálva**.  
A feladatod: bővíteni a teszteket, implementálni a logikát, és a munkát Git-ben követni.

---

## Az adatszerkezet működése

A `Screening` osztály egy `List<string>` listát és egy `int` értéket használ:

- `_bookedNames` — a foglalással rendelkező személyek neve, egy bejegyzés személyenként
- `_totalSeats` — a vetítés összes férőhelye, a konstruktorban kerül beállításra

A szabad helyek száma mindig kiszámolható: `_totalSeats - _bookedNames.Count`  
**Foglalásnál** a személy neve bekerül a `_bookedNames` listába.  
**Lemondásnál** a személy neve eltávolításra kerül a `_bookedNames` listából.

---

## Fontos szabályok (olvasd el implementáció előtt!)

- `BookSeat()` — foglalás csak akkor sikeres, ha van még szabad hely és a személy még nem foglalt
- `CancelBooking()` — csak akkor sikeres, ha a személy neve szerepel a `_bookedNames` listában
- `GetAvailableSeats()` — mindig `_totalSeats - _bookedNames.Count` alapján számolandó
- `IsHouseFull()` — akkor igaz, ha `_bookedNames.Count` egyenlő `_totalSeats`-szel

---

## Projektstruktúra

```
CinemaExam/
├── CinemaApp/                   ← Console projekt (ezt implementálod)
│   ├── Screening.cs
│   └── Program.cs
└── CinemaApp.Tests/             ← MSTest projekt (ezt bővíted)
    └── ScreeningTests.cs
```

---

## Lépések

### 1. lépés — Klónozás és megnyitás
Hozz létre egy repót klónozd le és rakd bele a kibontott zip fájljait (ez a readme jelenjen majd meg a githubon), és nyisd meg a `.sln` fájlt Visual Studio 2022-ben.

### 2. lépés — Futtasd a teszteket (mind el kell, hogy bukjon)
Nyisd meg a **Test Explorer**-t és futtass minden tesztet.  
📸 **Készíts képernyőképet a bukó tesztekről.** Ez lesz a „before" képed.
A kép is kerüljön bele a repóba a saját névvel ellátva
Példa: kovacs_bela_test_before.png

### 3. lépés — Bővítsd a teszteket
Minden tesztmetódus alatt `// TODO` kommentek jelzik, milyen eseteket kell még lefedned.  
Írj új tesztmetódusokat az edge case-ekre, mielőtt bármit implementálsz.

```
git add .
git commit -m "Edge case tesztek hozzáadva implementáció előtt"
```

(vagy a visual studio-ban a grafikus felületen)

### 4. lépés — Implementáld az osztályt
Javasolt sorrend:
1. Konstruktor és `GetTitle`
2. `BookSeat` és `GetAvailableSeats`
3. `CancelBooking` és `IsBooked`
4. `GetBookedCount` és `IsHouseFull`

**Minden metódus (vagy kis csoport) után commitolj:**
```
git commit -m "BookSeat és GetAvailableSeats implementálva"
```

### 5. lépés — Minden teszt zöld
📸 **Készíts képernyőképet az összes átmenő tesztről.** Ez lesz az „after" képed.

---

## Extra feladat — Várólista (nem kötelező)

Ha az alap feladattal végzett, bővítsd a `Screening` osztályt várólistás funkcionalitással.  
A várólista egy `_waitingList` nevű `List<string>` lista, amelyet szintén a konstruktorban kell inicializálni.

### A várólista szabályai

- Várólistára csak akkor lehet felkerülni, ha a vetítés teli (`IsHouseFull()` igaz)
- Ha valaki lemond a foglalását (`CancelBooking()`), az első várólistás személy automatikusan megkapja a helyet és kikerül a várólistáról
- Egy személy egyszerre csak egyszer szerepelhet a várólistán
- Már foglalással rendelkező személy nem kerülhet várólistára

### Implementálandó metódusok

| Metódus | Leírás |
|---|---|
| `AddToWaitingList(string name)` | Felveszi a személyt a várólista végére — visszatér false ha a feltételek nem teljesülnek |
| `RemoveFromWaitingList(string name)` | Eltávolítja a személyt a várólistáról — visszatér false ha nem szerepelt rajta |
| `IsOnWaitingList(string name)` | Visszatér true ha a személy szerepel a várólistán |
| `GetWaitingListCount()` | A várólistán lévő személyek száma |
| `GetWaitingPosition(string name)` | A személy pozíciója a várólistán (1-től számozva) — -1 ha nem szerepel rajta |

### Módosítandó metódus

- `CancelBooking()` — lemondás után automatikusan előlépteti a várólista első személyét, ha van várakozó

### Extra tesztek

Írj teszteket minden új metódushoz a megszokott módon, beleértve az edge case-eket:
- Várólistára kerülés szabad helyek esetén
- Ugyanaz a személy kétszer próbál felkerülni
- Lemondás utáni automatikus előléptetés
- Pozíció lekérése nem létező személyre

---

