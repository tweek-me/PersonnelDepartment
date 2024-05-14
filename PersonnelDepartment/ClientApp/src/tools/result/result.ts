//TASK переписать
export class Result {
	public isSuccess = this.errors.length === 0;

	constructor(
		public errors: string[]
	) { }

	public static success(): Result {
		return new Result([]);
	}

	public static failed(errors: string[]): Result {
		return new Result(errors);
	}
}

export class FailResult {
	public isSuccess: false = false;
	public data: null = null;
	public errors: string[];

	constructor(errors: string[]) {
		this.errors = errors;
	}
}

export class SuccessResult {
	public isSuccess: true = true;
	public data: null = null;
	public errors: string[] = [];
}