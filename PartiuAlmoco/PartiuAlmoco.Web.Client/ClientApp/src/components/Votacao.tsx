import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as VotacaoStore from '../store/Votacao';

type VotacaoProps =
    VotacaoStore.VotacaoState &
    typeof VotacaoStore.actionCreators &
    RouteComponentProps<{}>;

class Votacao extends React.PureComponent<VotacaoProps> {
    public render() {
        return (
            <React.Fragment>
                <h1>Votação</h1>

                <p aria-live="polite">Contagem de Votos Atual: <strong>{this.props.votos}</strong></p>

                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => { this.props.increment(); }}>
                    Increment
                </button>
            </React.Fragment>
        );
    }
};

export default connect(
    (state: ApplicationState) => state.votacao,
    VotacaoStore.actionCreators
)(Votacao);
