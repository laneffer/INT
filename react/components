import React, { useState, useEffect } from 'react';
import { Row, Col, Card } from 'react-bootstrap';
import debug from 'sabio-debug';
import Table from './PollstersGradeTable';
import GradeTitle from './PollstersGradeTitle';
import PropTypes from 'prop-types';
import classNames from 'classnames';
import './pollstersgrade.css';
import * as pollsterGradeService from '../../services/pollsterGradeService';

const _logger = debug.extend('PollstersGrades');

const GradeColumn = ({ row }) => {
    return (
        <>
            <span className={classNames('color-' + row.original.grade)}>{row.original.grade}</span>
        </>
    );
};

const PollsterColumn = (props) => {
    return <div className="boldPollster">{props.row.original.pollster}</div>;
};

const NameColumn = ({ row }) => {
    return <div className="boldName">{row.original.name}</div>;
};

function Grades() {
    const [gradeData, setGradeData] = useState([]);
    const [dataByPollster, setByPollster] = useState([]);
    const [selectedGrade, setSelectedGrade] = useState(1); //show 1, 226, 227

    useEffect(() => {
        _logger('useEffect', gradeData);
        pollsterGradeService.getGradesByElectionId(selectedGrade).then(onGetGradesSuccess).catch(onGetGradesError);
    }, [selectedGrade]);

    const onGetGradesSuccess = (response) => {
        _logger('onGetGradesSuccess', response);
        let grade = response.items;

        const dataByPollster = selectByPollster(response.items);
        _logger('POLLSTERS', dataByPollster);

        setGradeData(grade);
        setByPollster(dataByPollster);
    };

    const onElectionChange = (e) => {
        const selectedElection = e.target.value;
        setSelectedGrade(selectedElection);
    };

    const getLetter = (gradeNum) => {
        let letter = '';
        if (gradeNum <= 3.0) {
            letter = 'A';
        } else if (gradeNum <= 3.5 && gradeNum > 3.0) {
            letter = 'A-';
        } else if (gradeNum <= 4.0 && gradeNum > 3.5) {
            letter = 'B+';
        } else if (gradeNum <= 4.5 && gradeNum > 4.0) {
            letter = 'B';
        } else if (gradeNum <= 5.0 && gradeNum > 4.5) {
            letter = 'B-';
        } else if (gradeNum <= 5.5 && gradeNum > 5.0) {
            letter = 'C+';
        } else if (gradeNum <= 6.0 && gradeNum > 5.5) {
            letter = 'C';
        } else if (gradeNum <= 6.5 && gradeNum > 6.0) {
            letter = 'D';
        } else {
            letter = 'F';
        }
        return letter;
    };

    const selectByPollster = (results) => {
        const data = [];
        const pollsters = [];

        results.forEach((result) => {
            if (!pollsters.some((pollster) => pollster === result.pollsterId)) {
                pollsters.push(result.pollsterId);
                const filteredPollster = results.filter((current) => current.pollsterId === result.pollsterId);

                const pollsterData = {
                    pollsterId: result.pollsterId,
                    name: result.pollster,
                    totalPolls: filteredPollster.reduce((prev, current) => prev + current.totalPolls, 0),
                    totalDiff: (
                        filteredPollster.reduce((prev, current) => prev + Math.sqrt(current.diff * current.diff), 0) /
                        filteredPollster.length
                    ).toFixed(2),
                };
                pollsterData.grade = getLetter(pollsterData.totalDiff);
                data.push(pollsterData);
            }
        });
        return data;
    };

    const onGetGradesError = (err) => {
        _logger('onGetGradesError', err);
    };

    const columns = [
        {
            Header: 'Pollster Id',
            accessor: 'pollsterId',
            sort: true,
        },
        {
            Header: 'Name',
            accessor: 'name',
            sort: true,
            Cell: NameColumn,
        },
        {
            Header: 'Total Polls',
            accessor: 'totalPolls',
            sort: true,
        },
        {
            Header: 'Total % Difference',
            accessor: 'totalDiff',
            sort: true,
        },
        {
            Header: 'Grade',
            accessor: 'grade',
            sort: true,
            Cell: GradeColumn,
        },
    ];

    const columnsTwo = [
        {
            Header: 'Pollster Id',
            accessor: 'pollsterId',
            sort: true,
        },
        {
            Header: 'Pollster',
            accessor: 'pollster',
            sort: true,
            Cell: PollsterColumn,
        },
        {
            Header: 'Final Result Difference',
            accessor: 'diff',
            sort: true,
        },
        {
            Header: 'Candidate Id',
            accessor: 'candidateId',
            sort: true,
        },
        {
            Header: 'Polls Analyzed',
            accessor: 'totalPolls',
            sort: true,
        },
        {
            Header: 'Average %',
            accessor: 'avgpct',
            sort: true,
        },
    ];

    const sizePerPageList = [
        {
            text: '10',
            value: 10,
        },
        {
            text: '15',
            value: 15,
        },
        {
            text: '20',
            value: 20,
        },
    ];

    return (
        <>
            <div className="container mb-5 mt-5">
                <GradeTitle
                    breadCrumbItems={[
                        {
                            label: 'Pollsters Grades',
                            path: '/pollsters/grades',
                            active: true,
                        },
                    ]}
                    title={'Pollsters Grades by Election'}
                />
                <div className="electionSelection mb-2">
                    <select
                        className="electSelect form-select form-select-m me-2 list-group-item-action text-dark"
                        value={selectedGrade}
                        onChange={onElectionChange}>
                        <option value="electionYear">Election Year:</option>
                        <option value="1">Baja California 2021</option>
                        <option value="226">Durango 2022</option>
                        <option value="227">Hidalgo 2022</option>
                    </select>
                </div>
                <Row>
                    <Col xs={12}>
                        <Card className="gradesTables">
                            <Card.Body>
                                <Table
                                    columns={columns}
                                    data={dataByPollster}
                                    pageSize={10}
                                    sizePerPageList={sizePerPageList}
                                    isSortable={true}
                                    isPagination={true}
                                    isSearchable={true}
                                    isSelectable={true}
                                />
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </div>
            <div className="container mb-5 mt-5">
                <Row>
                    <Col>
                        <div className="page-title-box">
                            <h4 className="page-title">Pollsters Analyzed Per Candidate by Election</h4>
                        </div>
                    </Col>
                </Row>
                <Row>
                    <Col xs={12}>
                        <Card className="PollsAnalysisTable">
                            <Card.Body>
                                <Table
                                    columns={columnsTwo}
                                    data={gradeData}
                                    pageSize={10}
                                    sizePerPageList={sizePerPageList}
                                    isSortable={true}
                                    isPagination={true}
                                    isSearchable={true}
                                    isSelectable={true}
                                />
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </div>
        </>
    );
}

GradeColumn.propTypes = {
    row: PropTypes.shape({
        original: PropTypes.shape({
            grade: PropTypes.string,
        }),
    }),
};

PollsterColumn.propTypes = {
    row: PropTypes.shape({
        original: PropTypes.shape({
            pollster: PropTypes.string,
        }),
    }),
};

NameColumn.propTypes = {
    row: PropTypes.shape({
        original: PropTypes.shape({
            name: PropTypes.string,
        }),
    }),
};

export default Grades;
