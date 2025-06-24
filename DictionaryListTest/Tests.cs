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

    [Test]
    public void IndexAccess()
    {
        // tests that out-of-bounds access throws exceptions
        _dictList = new DictionaryList<int>(5);
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = _dictList[0]);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = _dictList[-4]);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = _dictList[99]);
            Assert.Throws<ArgumentOutOfRangeException>(() => _dictList[1] = 4);
            Assert.Throws<ArgumentOutOfRangeException>(() => _dictList[-7] = 5);
            Assert.Throws<ArgumentOutOfRangeException>(() => _dictList[99] = 6);
            Assert.Throws<ArgumentOutOfRangeException>(() => _dictList[0] = 9);
        });
    }

    [Test]
    public void MultipleUnset()
    {
        // tests that in-bounds removal is OK
        _dictList.Add(1);
        _dictList.Add(2);
        _dictList.Add(3);
        _dictList.Unset(1);
        _dictList.Unset(1);
        _dictList.Unset(1);
        Assert.Pass();
    }

    [Test]
    public void ListTraversal()
    {
        // correct traversal order, even when some items are gone
        _dictList.Add(1);
        _dictList.Add(2);
        _dictList.Add(3);
        _dictList.Add(4);
        _dictList.Add(5);
        _dictList.Unset(1);

        var keyQueue = new Queue<int>();
        var valQueue = new Queue<int>();
        foreach (var kv in _dictList)
        {
            keyQueue.Enqueue(kv.Key);
            valQueue.Enqueue(kv.Value);
        }

        // pop from the queue
        Assert.That(keyQueue.Dequeue(), Is.EqualTo(0));
        Assert.That(keyQueue.Dequeue(), Is.EqualTo(2));
        Assert.That(keyQueue.Dequeue(), Is.EqualTo(3));
        Assert.That(keyQueue.Dequeue(), Is.EqualTo(4));
        Assert.That(keyQueue.TryPeek(out _), Is.EqualTo(false));

        Assert.That(valQueue.Dequeue(), Is.EqualTo(1));
        Assert.That(valQueue.Dequeue(), Is.EqualTo(3));
        Assert.That(valQueue.Dequeue(), Is.EqualTo(4));
        Assert.That(valQueue.Dequeue(), Is.EqualTo(5));
        Assert.That(keyQueue.TryPeek(out _), Is.EqualTo(false));
    }
}
