using Utils;
using static PartOne;

internal class PartTwo : PartBase<long>
{
    public override long Run(string[] input)
    {
        Cards['J'] = '0';

        long result = 0;

        List<(string, int)> handValueWithBids = new List<(string, int)>();
        foreach (var line in input)
        {
            string[] game = line.Split(' ');

            string hand = game[0];
            int bid = int.Parse(game[1]);

            HandType type = GetType(hand);

            string handValue = ConvertToValue(hand, type);

            handValueWithBids.Add((handValue, bid));
        }

        foreach (var bidWithIdx in handValueWithBids.OrderBy(x => x.Item1).Select((x, i) => new { Hand = x.Item1, Bid = x.Item2, Idx = i }))
        {
            result += bidWithIdx.Bid * (bidWithIdx.Idx + 1);
        }

        return result;
    }

    private HandType GetType(string hand)
    {
        int jokersCount = hand.Count(c => c == 'J');
        if(jokersCount == 5)
        {
            return HandType.FiveOfKind;
        }

        var type = hand.GroupBy(x => x).Where(x => x.Key != 'J').ToArray();
        int[] counts = type.Select(x => x.Count()).ToArray();

        HandType handType;
        if (counts.Contains(5))
        {
            return HandType.FiveOfKind;
        }
        else if (counts.Contains(4))
        {
            handType = HandType.FourOfKind;
        }
        else if (counts.Contains(3) && counts.Contains(2))
        {
            handType = HandType.FullHouse;
        }
        else if (counts.Contains(3))
        {
            handType = HandType.ThreeOfKind;
        }
        else if (counts.Count(x => x == 2) == 2)
        {
            handType = HandType.TwoPair;
        }
        else if (counts.Count(x => x == 2) == 1)
        {
            handType = HandType.OnePair;
        }
        else if (counts.All(x => x == 1))
        {
            handType = HandType.HighCard;
        }
        else
        {
            throw new InvalidOperationException();
        }

        if (jokersCount == 0)
            return handType;

        switch (handType)
        {
            case HandType.FourOfKind:
                if (jokersCount == 1)
                    return HandType.FiveOfKind;
                else
                    throw new InvalidOperationException();
            case HandType.ThreeOfKind:
                if(jokersCount == 1)
                    return HandType.FourOfKind;
                else if(jokersCount == 2)
                    return HandType.FiveOfKind;
                else
                    throw new InvalidOperationException();
            case HandType.TwoPair:
                if (jokersCount == 1)
                    return HandType.FullHouse;
                else
                    throw new InvalidOperationException();
            case HandType.OnePair:
                if (jokersCount == 1)
                    return HandType.ThreeOfKind;
                else if (jokersCount == 2)
                    return HandType.FourOfKind;
                else if (jokersCount == 3)
                    return HandType.FiveOfKind;
                else
                    throw new InvalidOperationException();
            case HandType.HighCard:
                if (jokersCount == 1)
                    return HandType.OnePair;
                else if (jokersCount == 2)
                    return HandType.ThreeOfKind;
                else if (jokersCount == 3)
                    return HandType.FourOfKind;
                else if (jokersCount == 4)
                    return HandType.FiveOfKind;
                else
                    throw new InvalidOperationException();
            default:
                return handType;
        }
    }
}