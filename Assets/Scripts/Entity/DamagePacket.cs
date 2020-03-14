namespace PipelineDreams
{
    using MutableValue;
    public struct DamagePacket
    {
        /// <summary>
        /// The entity which performed this attack
        /// </summary>
        public Entity subject;
        public FunctionChain damage;
        public FunctionChain accuracy;

    }
}