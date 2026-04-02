public interface IBikeElement
{
    // Entry point for Visitor
    void Accept(IVisitor visitor);
}