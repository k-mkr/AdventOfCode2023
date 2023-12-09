using Utils;

internal class PartOne : PartBase<long>
{
    public enum HandType
    {
        HighCard = 1,
        OnePair,
        TwoPair,
        ThreeOfKind,
        FullHouse,
        FourOfKind,
        FiveOfKind
    }

    public static Dictionary<char, char> Cards = new()
    {
        { 'T', 'A' },
        { 'J', 'B' },
        { 'Q', 'C' },
        { 'K', 'D' },
        { 'A', 'E' }
    };

    public override long Run(string[] input)
    {
        long result = 0;

        List<(string, int)> handValueWithBids = new List<(string, int)>();
        foreach(var line in input)
        {
            string[] game = line.Split(' ');
            
            string hand = game[0];
            int bid = int.Parse(game[1]);

            HandType type = GetType(hand);

            string handValue = ConvertToValue(hand, type);

            handValueWithBids.Add((handValue, bid));
        }

        foreach(var bidWithIdx in handValueWithBids.OrderBy(x => x.Item1).Select((x, i) => new { Hand = x.Item1, Bid = x.Item2, Idx = i }))
        {
            result += bidWithIdx.Bid * (bidWithIdx.Idx + 1);
        }

        return result;
    }

    private HandType GetType(string hand)
    {
        var type = hand.GroupBy(x => x).ToArray();
        int[] counts = type.Select(x => x.Count()).ToArray();

        if (counts.Contains(5))
        {
            return HandType.FiveOfKind;
        }
        else if(counts.Contains(4))
        {
            return HandType.FourOfKind;
        }
        else if(counts.Contains(3) && counts.Contains(2))
        {
            return HandType.FullHouse;
        }
        else if(counts.Contains(3))
        {
            return HandType.ThreeOfKind;
        }
        else if(counts.Count(x => x == 2) == 2)
        {
            return HandType.TwoPair;
        }
        else if(counts.Count(x => x == 2) == 1)
        {
            return HandType.OnePair;
        }
        else if(counts.All(x => x == 1))
        {
            return HandType.HighCard;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public static string ConvertToValue(string hand, HandType handType)
    {
        return (int)handType + new string(hand.Select(x => char.IsDigit(x) ? x : Cards[x]).ToArray());
    }
}