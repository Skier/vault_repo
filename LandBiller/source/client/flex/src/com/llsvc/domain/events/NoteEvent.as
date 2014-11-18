package com.llsvc.domain.events
{
	import com.llsvc.domain.Note;
	
	import flash.events.Event;

	public class NoteEvent extends Event
	{
		public static const NOTE_IS_LOADED:String = "noteIsLoaded";
		
		public var note:Note;
		
		public function NoteEvent(type:String, note:Note, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.note = note;
		}
		
		override public function clone():Event 
		{
			return new NoteEvent(type, note, bubbles, cancelable);
		}
		
	}
}
