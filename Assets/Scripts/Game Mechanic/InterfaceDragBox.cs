public interface InterfaceDragBox
{
	float getdistPerObj { get; }

	float getsizeOfBox { get; }

	int getBlockID { get; }

	void ReturnBlocks();

	void PlaySo(bool n);

	bool getRbStatusFall();
}
