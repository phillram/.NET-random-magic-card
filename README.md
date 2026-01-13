# Magic: The Gathering Random Card Fetcher (.NET)

A .NET console application that fetches a random Magic: The Gathering card from the Scryfall API and displays all its details.

## Prerequisites

- .NET SDK 8.0 or later

## Installation

1. Navigate to the project directory:
   ```bash
   cd mtg-random-card
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

## Running the Application

Execute the application with:

```bash
dotnet run
```

Or build and run:

```bash
dotnet build
dotnet bin/Debug/net8.0/mtg-random-card
```

## Features

The application displays the following card details:
- Card name and mana cost
- Converted mana cost (CMC)
- Type line (creature, sorcery, etc.)
- Rarity
- Set name and code
- Artist name
- Oracle text (card rules)
- Flavor text
- Power/Toughness (for creatures)
- Loyalty (for planeswalkers)
- Colors and color identity
- Keywords
- Card image URL

## API

This application uses the [Scryfall API](https://scryfall.com/docs/api), which provides a free and open source database of Magic: The Gathering cards.

## Example Output

```
==================================================
Card Name: Towering Indrik
Mana Cost: {3}{G}
Converted Mana Cost: 4
Type Line: Creature — Beast
Rarity: common
Set: Return to Ravnica (rtr)
Artist: Lars Grant-West

Oracle Text:
Reach (This creature can block creatures with flying.)

Flavor Text:
It chases its airborne prey relentlessly, heedless to what it pulverizes beneath its hooves.

Power/Toughness: 2/4
Colors: G
Color Identity: G
Keywords: Reach

Image URL: https://cards.scryfall.io/normal/front/c/6/...
==================================================
```
# .NET-random-magic-card
