import { Error } from "../errors/error";

export class Result {
	public isSuccess = this.errors.length === 0;

	constructor(
		public errors: Error[]
	) { }

	public static success(): Result {
		return new Result([]);
	}

	public static failed(errors: Error[]): Result {
		return new Result(errors);
	}
}

export class FailResult {
	public isSuccess: false = false;
	public data: null = null;
	public errors: Error[];

	public get errorsAsString() {
		return this.errors[0].errorMessage;
	}

	constructor(errors: Error[]) {
		this.errors = errors;
	}
}

export class SuccessResult {
	public isSuccess: true = true;
	public data: null = null;
	public errors: Error[] = [];
}