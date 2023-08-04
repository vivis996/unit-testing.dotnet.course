namespace TestNinja.Tests;

[TestFixture]
public class StackTests
{
    private Fundamentals.Stack<string?> _stack;

    [SetUp]
    public void SetUp()
    {
        this._stack = new Fundamentals.Stack<string?>();
    }

    [Test]
    public void Push_ArgIsNull_ThrowArgumentNullException()
    {
        Assert.That(() => this._stack.Push(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Push_ValidArg_AddTheObjectToTheStack()
    {
        this._stack.Push("a");

        Assert.That(this._stack.Count, Is.EqualTo(1));
    }

    [Test]
    public void Count_EmptyStack_ReturnZero()
    {
        Assert.That(this._stack.Count, Is.EqualTo(0));
    }

    [Test]
    public void Pop_EmptyStack_ThrowInvalidOperationException()
    {
        Assert.That(() => this._stack.Pop(), Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_StackWithAFewObjects_ReturnObjectOnTheTop()
    {
        // Arrange
        this._stack.Push("a");
        this._stack.Push("b");
        this._stack.Push("c");

        // Act
        var result = this._stack.Pop();

        // Assert
        Assert.That(result, Is.EqualTo("c"));
    }

    [Test]
    public void Pop_StackWithAFewObjects_RemoveThisObjectOnTheTop()
    {
        // Arrange
        this._stack.Push("a");
        this._stack.Push("b");
        this._stack.Push("c");

        // Act
        this._stack.Pop();

        // Assert
        Assert.That(this._stack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Peek_EmptyStack_ThrowInvalidOperationException()
    {
        Assert.That(() => this._stack.Peek(), Throws.InvalidOperationException);
    }

    [Test]
    public void Peek_StackWithObjects_ReturnObjectOnTopOfTheStack()
    {
        // Arrange
        this._stack.Push("a");
        this._stack.Push("b");
        this._stack.Push("c");

        // Act
        var result = this._stack.Peek();

        // Assert
        Assert.That(result, Is.EqualTo("c"));
    }

    [Test]
    public void Peek_StackWithObjects_DoesNotRemoveTheObjectOnTopOfTheStack()
    {
        // Arrange
        this._stack.Push("a");
        this._stack.Push("b");
        this._stack.Push("c");

        // Act
        this._stack.Peek();

        // Assert
        Assert.That(this._stack.Count, Is.EqualTo(3));
    }
}
