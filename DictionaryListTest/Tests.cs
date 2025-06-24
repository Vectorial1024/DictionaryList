using Vectorial1024.Collections.Generic;

namespace DictionaryListTest;

public class Tests
{
    // for convenience, we will perform tests on integer lists
    private DictionaryList<int> _dictList;

    [SetUp]
    public void Setup()
    {
        _dictList = new DictionaryList<int>();
    }

    [Test]
    public void BasicReadWrite()
    {
        // nothing yet, so 0
        Assert.That(_dictList, Has.Count.EqualTo(0));
        const int itemsTarget = 5;

        // add in things, so this should increase the count
        for (var i = 0; i < itemsTarget; i++)
        {
            _dictList.Add(i);
            Assert.Multiple(() =>
            {
                Assert.That(_dictList, Has.Count.EqualTo(i + 1));
                Assert.That(_dictList.Count, Is.EqualTo(i + 1));
                Assert.That(_dictList.ContainsIndex(i), Is.True);
                Assert.That(_dictList[i], Is.EqualTo(i));
            });
        }

        // remove things, to decrease the count
        for (var i = 0; i < itemsTarget; i++)
        {
            _dictList.Unset(i);
            Assert.Multiple(() =>
            {
                Assert.That(_dictList, Has.Count.EqualTo(itemsTarget - i - 1));
                Assert.That(_dictList.Count, Is.EqualTo(itemsTarget - i - 1));
                Assert.That(_dictList.ContainsIndex(i), Is.False);
                Assert.Throws<ArgumentOutOfRangeException>(() => _ = _dictList[i]);
            });
            Assert.That(_dictList.ContainsIndex(i), Is.False);
        }
    }
}
