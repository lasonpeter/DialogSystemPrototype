using XNode;

namespace DialogSystem
{
    public interface ITraversable
    {
        /// <summary>
        /// Performs the required actions and returns the next node 
        /// </summary>
        /// <param name="chosenIndex">In case there is player input, this value is used to determine the output</param>
        /// <returns>Refrence to next node</returns>
        Node NextNode(int chosenIndex);
    }
}