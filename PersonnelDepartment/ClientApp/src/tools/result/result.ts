//TASK ILYA переписать
export class Result<T> {
	public isSuccess = this.errors.length === 0;

	constructor(
		public data: T | null,
		public errors: string[],
	) { }

	public static success<T>(value: T | null = null): Result<T> {
		return new Result<T>(value, []);
	}

	public static failed(errors: string[]): Result<null> {
		return new Result(null, errors);
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