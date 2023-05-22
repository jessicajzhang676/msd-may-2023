# Job Listings API

This internal API is responsible for creating and maintaining job listings.

A job listing is for an active (non-retired) Job from the Jobs API.

## Responsibilities

1. The *primary* responsibility is to provide a list of currently open job listings.
2. Job listings can be created with this API.
    - Job listings can only be added for Jobs that are current.
3. Job listings can be *closed* by this API.
    - Job Listing is filled
    - Job is unlisted as a result of the Job being retired.