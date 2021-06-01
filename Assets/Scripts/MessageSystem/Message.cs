public abstract class Message
{
	private int _referenceCount = 0;

	public void Init(int numberOfReceivers)
	{
		_referenceCount = numberOfReceivers;
	}

	public void OnDoneUsing()
	{
		_referenceCount--;
		
		if (_referenceCount == 0)
		{
			MessageProvider.RecycleMessage(this);
		}
	}
	
}
