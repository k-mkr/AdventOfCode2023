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

            HandType type = GetTypeWithJoker(hand);

            string handValue = ConvertToValue(hand, type);

            handValueWithBids.Add((handValue, bid));
        }

        foreach (var bidWithIdx in handValueWithBids.OrderBy(x => x.Item1).Select((x, i) => new { Hand = x.Item1, Bid = x.Item2, Idx = i }))
        {
            result += bidWithIdx.Bid * (bidWithIdx.Idx + 1);
        }

        return result;
    }

    private HandType GetTypeWithJoker(string hand)
    {
        int jokersCount = hand.Count(c => c == 'J');
        if(jokersCount == 5)
        {
            return HandType.FiveOfKind;
        }

        string handWithoutJoker = hand.Replace("J", "");
        
        HandType handType = PartOne.GetType(handWithoutJoker);

        if (jokersCount == 0)
            return handType;

        switch (handType, jokersCount)
        {
            case (HandType.FourOfKind, 1):
                return HandType.FiveOfKind;
            case (HandType.ThreeOfKind, 1):
                return HandType.FourOfKind;
            case (HandType.ThreeOfKind, 2):
                return HandType.FiveOfKind;
            case (HandType.TwoPair, 1):
                return HandType.FullHouse;
            case (HandType.OnePair, 1):
                return HandType.ThreeOfKind;
            case (HandType.OnePair, 2):
                return HandType.FourOfKind;
            case (HandType.OnePair, 3):
                return HandType.FiveOfKind;
            case (HandType.HighCard, 1):
                return HandType.OnePair;
            case (HandType.HighCard, 2):
                return HandType.ThreeOfKind;
            case (HandType.HighCard, 3):
                return HandType.FourOfKind;
            case (HandType.HighCard, 4):
                return HandType.FiveOfKind;
            default:
                return handType;
        }
    }
}