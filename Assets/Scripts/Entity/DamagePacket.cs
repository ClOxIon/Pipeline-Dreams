namespace PipelineDreams{
public struct DamagePacket
{
        /// <summary>
        /// The entity which performed this attack
        /// </summary>
    public Entity subject;
    public int damage;
    public float accuracy;
}
}