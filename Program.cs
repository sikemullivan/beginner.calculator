char[] allowedOperators = new char[] { 'x', '*', '/', '+', '-' };
STARTOVER:
Console.WriteLine("Yo, write a problem:");
string org = Console.ReadLine();
string line = org;

for (int i = 0; i < line.Length; i++)
{
    char c = line[i];
    if (allowedOperators.Contains(c))
    {
        if (i == 0)
        {
            Console.WriteLine("Can't start with an operator.");
            goto STARTOVER;
        }
        else if (i == line.Length - 1)
        {
            Console.WriteLine("Can't end with an operator.");
            goto STARTOVER;
        }
        else if (line[i - 1] != ' ')
        {
            line = line.Insert(i, " ");
        }
        else if (line[i + 1] != ' ')
        {
            line = line.Insert(i + 1, " ");
        }
    }
}

//if (line != org)
//{
//    Console.WriteLine($"Cleaned: {line}");
//    goto STARTOVER;
//}

string[] segments = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

List<decimal> values = new List<decimal>();
List<char> operators = new List<char>();

if (segments.Length == 0)
{
    goto STARTOVER;
}

if (segments.Length % 2 == 0)
{
    Console.WriteLine($"Your problem doesn't make sense. Missing last number.");
    goto STARTOVER;
}

//validation
for (int i = 0; i < segments.Length; i++)
{
    var value = segments[i];
    var even = i % 2 == 0;
    if (even)
    {
        if (decimal.TryParse(value, out decimal d))
        {
            values.Add(d);
        }
        else
        {
            Console.WriteLine($"Your problem doesn't make sense. Failed on number {value}");
            goto STARTOVER;
        }
    }
    else
    {
        if (allowedOperators.Contains(value.ToLower()[0]))
        {
            operators.Add(value.ToLower()[0]);
        }
        else
        {
            Console.WriteLine($"Your problem doesn't make sense. Failed on operator {value}");
            goto STARTOVER;
        }
    }
}

for (int i = 0; i < operators.Count; i++)
{
    switch (operators[i])
    {
        case 'x':
        case '*':
            values[i + 1] = values[i] * values[i + 1];
            values.RemoveAt(i);
            operators.RemoveAt(i);
            i -= 1;
            break;
        case '/':
            if (values[i + 1] == 0)
            {
                Console.WriteLine("You can't divide by 0.");
                goto STARTOVER;
            }
            values[i + 1] = values[i] / values[i + 1];
            values.RemoveAt(i);
            operators.RemoveAt(i);
            i -= 1;
            break;
        default:
            break;
    }
}

var result = values[0];
for (int i = 0; i < operators.Count; i += 1)
{
    switch (operators[i])
    {
        case '+':
            result = result + values[i + 1];
            break;
        case '-':
            result = result - values[i + 1];
            break;
        default:
            Console.WriteLine("Uknown operator.");
            goto STARTOVER;
    }
}
Console.WriteLine($"Answer: {result}");
goto STARTOVER;